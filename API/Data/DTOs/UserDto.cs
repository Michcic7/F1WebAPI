namespace API.Data.DTOs;

public record UserDto
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}
