namespace API.Data.Models;

public record RaceResult
{
    public required int RaceResultId { get; init; }
    public required int Year { get; init; }
    public required int Position { get; init; }
    public required int CircuitId { get; init; }
    public required Circuit Circuit { get; init; }
    public required DateOnly Date { get; init; }
    public required int DriverId { get; init; }
    public required Driver Driver { get; init; }
    public required int TeamId { get; init; }
    public required Team Team { get; init; }
    public required int Laps { get; init; }
    public required string Time { get; init; }
    public required float Points { get; init; }
}
