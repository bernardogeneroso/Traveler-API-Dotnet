namespace Models;

public class RefreshUserToken
{
    public Guid Id { get; set; }
    public AppUser User { get; set; } = new AppUser();
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; } = DateTime.UtcNow.AddDays(7);
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public DateTime? Revoked { get; set; }
    public bool IsActive => Revoked == null && !IsExpired;
}
