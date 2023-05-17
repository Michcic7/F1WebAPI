namespace API.Data.Models;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public List<RefreshToken>? RefreshTokens { get; set; }
}
