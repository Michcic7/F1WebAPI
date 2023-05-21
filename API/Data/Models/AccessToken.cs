namespace API.Data.Models;

public record AccessToken
{
    public required string Token { get; init; }
    public required DateTime Expiry { get; init; }
}
