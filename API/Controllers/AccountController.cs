using System.Text;
using API.DTOs;
using API.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly TokenService _tokenService;
    private readonly IUserAccessor _userAccessor;
    private readonly IImageAccessor _imageAccessor;
    private readonly IMailAccessor _mailAccessor;
    private readonly IConfiguration _config;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration config, TokenService tokenService, IUserAccessor userAccessor, IImageAccessor imageAccessor, IMailAccessor mailAccessor)
    {
        _config = config;
        _mailAccessor = mailAccessor;
        _imageAccessor = imageAccessor;
        _userAccessor = userAccessor;
        _tokenService = tokenService;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null) return Unauthorized("Invalid email or password");

        if (!user.EmailConfirmed) return Unauthorized("Email not confirmed");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) return Unauthorized("Invalid email or password");

        await SetRefreshToken(user);

        var origin = Request.Headers["Origin"];

        var userDto = CreateUserObject(user, origin);

        if (userDto == null) return Unauthorized();

        return userDto;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
        {
            ModelState.AddModelError("email", "Email taken");

            return ValidationProblem();
        }

        if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.Username))
        {
            ModelState.AddModelError("username", "Username taken");

            return ValidationProblem();
        }

        var user = new AppUser
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName = registerDto.Username
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded) return BadRequest("Problem registering user");

        var origin = Request.Headers["Origin"];
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        var verifyUrl = $"{origin}/account/verifyEmail?token={token}&email={user.Email}";

        var button = new MailButton
        {
            Text = "Verify Email",
            Link = verifyUrl
        };

        var body = $"Please verify your email by clicking on the button below.";

        await _mailAccessor.SendMail(user.Email, "RentX - Verify your email", user.DisplayName, button, body);

        return Ok("User registered");
    }

    [AllowAnonymous]
    [HttpPost("verifyEmail")]
    public async Task<IActionResult> VerifyEmail(string token, string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null) return Unauthorized();

        var decodedTokenBytes = WebEncoders.Base64UrlDecode(token);
        var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

        var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

        if (!result.Succeeded) return BadRequest("Could not verify email address");

        return Ok("Email verified - you can now login");
    }

    [AllowAnonymous]
    [HttpGet("resendEmailConfirmationLink")]
    public async Task<IActionResult> ResendEmailConfirmationLink(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null) return Unauthorized();

        if (user.EmailConfirmed) return Unauthorized("You are already authorized");

        var origin = Request.Headers["Origin"];
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        var verifyUrl = $"{origin}/account/verifyEmail?token={token}&email={user.Email}";

        var button = new MailButton
        {
            Text = "Verify Email",
            Link = verifyUrl
        };

        var body = $"Please verify your email by clicking on the button below.";

        await _mailAccessor.SendMail(user.Email, "RentX - Verify your email", user.DisplayName, button, body);

        return Ok("Email verification link resent");
    }

    [HttpPost("image")]
    public async Task<IActionResult> UploadAvatar([FromForm] IFormFile File)
    {
        var user = await _userManager.FindByEmailAsync(_userAccessor.GetEmail());

        if (user == null) return Unauthorized();

        if (user.AvatarPublicId != null)
        {
            var resultDeleteImage = await _imageAccessor.DeleteImage(user.AvatarPublicId);

            if (resultDeleteImage == null) return BadRequest("Problem uploading image");
        }

        var uploadResult = await _imageAccessor.AddImage(File, CancellationToken.None);

        if (uploadResult == null) return BadRequest("Problem uploading image");

        user.AvatarName = uploadResult.Filename;
        user.AvatarPublicId = uploadResult.PublicId;

        await _userManager.UpdateAsync(user);

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var user = await _userManager.FindByNameAsync(_userAccessor.GetUsername());

        if (user == null) return Unauthorized();

        await SetRefreshToken(user);

        var origin = Request.Headers["Origin"];

        var userDto = CreateUserObject(user, origin);

        if (userDto == null) return Unauthorized();

        return userDto;
    }

    [HttpPost("refreshToken")]
    public async Task<ActionResult<UserDto>> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        var user = await _userManager.Users
                .Include(x => x.RefreshTokens)
                .FirstOrDefaultAsync(x => x.Email == _userAccessor.GetEmail());

        if (user == null) return Unauthorized();

        var oldToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken);

        if (oldToken != null && !oldToken.IsActive) return Unauthorized();

        var origin = Request.Headers["Origin"];

        var userDto = CreateUserObject(user, origin);

        if (userDto == null) return Unauthorized();

        return userDto;
    }

    private async Task SetRefreshToken(AppUser user)
    {
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshTokens.Add(refreshToken);
        await _userManager.UpdateAsync(user);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7)
        };

        Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
    }

    private UserDto CreateUserObject(AppUser user, string origin)
    {
        var pathImage = user.AvatarName != null ? $"{_config.GetSection("Cloudinary").GetValue<string>("Url")}/{user.AvatarPublicId}" : null;

        if (user == null) return null;

        var userDto = new UserDto
        {
            DisplayName = user.DisplayName,
            Username = user.UserName,
            Token = _tokenService.CreateToken(user),
            Avatar = pathImage != null ? new AvatarDto
            {
                AvatarName = user.AvatarName,
                Url = pathImage,
                PublicId = user.AvatarPublicId
            } : null
        };

        return userDto;
    }
}
