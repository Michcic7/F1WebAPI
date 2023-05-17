using API.Data.Models;

namespace API.Data.DTOs;

public record RaceResultDto
{
    public required int Position { get; init; }
    public required string CircuitName { get; init; }
    public required DateOnly Date { get; init; }
    public required string DriverName { get; init; }
    public required string TeamName { get; init; }
    public required int Laps { get; init; }
    public required string Time { get; init; }
    public required float Points { get; init; }
}
