namespace API.Data.DTOs;

public record TeamStandingDto
{
    public required int Position { get; init; }
    public required string Name { get; init; }
    public required float Points { get; init; }
    public required int Year { get; init; }
}
