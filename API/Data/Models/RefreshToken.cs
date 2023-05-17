namespace API.Data.Models;

public class RefreshToken
{
    public int RefreshTokenId { get; set; }
    public string Content { get; set; }

    public User User { get; set; }
    public int UserId { get; set; }
}
