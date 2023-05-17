namespace API.Data.DTOs;

public record RefreshTokenDto
{
    public required string Content { get; init; }
}
