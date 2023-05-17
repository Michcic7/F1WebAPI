namespace API.Data.DTOs;

public record DriverStandingDto
{
    public required int Position { get; init; }
    public required string DriverName { get; init; }
    public required string Nationality { get; init; }
    public required string TeamName { get; init; }
    public required float Points { get; init; }
    public required int? Year { get; init; }
}
