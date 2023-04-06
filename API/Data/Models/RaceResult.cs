namespace API.Data.Models;

public class RaceResult
{
    public int RaceResultId { get; set; }
    public int Year { get; set; }
    public int Position { get; set; }
    public int CircuitId { get; set; }
    public Circuit Circuit { get; set; }
    public DateOnly Date { get; set; }
    public int DriverId { get; set; }
    public Driver Driver { get; set; }
    public int TeamId { get; set; }
    public Team Team { get; set; }
    public int Laps { get; set; }
    public string Time { get; set; }
    public float Points { get; set; }
}
