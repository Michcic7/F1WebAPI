namespace API.Data.Models;

public record RefreshToken
{
    public int RefreshTokenId { get; set; }
    public required string Content { get; init; }
    public required DateTime Expiry { get; init; }

    public required User User { get; init; }
    public required int UserId { get; init; }
}
