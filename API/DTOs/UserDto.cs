namespace API.DTOs;

public class UserDto
{

    public string DisplayName { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public Avatar? Avatar { get; set; } = null;
}

public class Avatar
{
    public string? AvatarName { get; set; } = null;
    public string? Url { get; set; } = null;
}