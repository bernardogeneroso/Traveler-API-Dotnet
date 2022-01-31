using Microsoft.AspNetCore.Identity;

namespace Models;

public class AppUser : IdentityUser
{
    public string DisplayName { get; set; } = string.Empty;
    public string? AvatarName { get; set; }
    public ICollection<RefreshUserToken> RefreshTokens { get; set; } = new List<RefreshUserToken>();
}
