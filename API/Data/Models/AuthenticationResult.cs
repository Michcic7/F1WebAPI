namespace API.Data.Models;

public record AuthenticationResult
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
    public required DateTime Expiry { get; init; }
}
