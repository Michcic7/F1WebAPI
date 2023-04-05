using API.Data.Models;

namespace API.Data.DTOs;

public class RaceResultDto
{
    public int Position { get; set; }
    public string CircuitName { get; set; }
    public DateOnly Date { get; set; }
    public string DriverName { get; set; }
    public string TeamName { get; set; }
    public int Laps { get; set; }
    public string Time { get; set; }
    public float Points { get; set; }
    public int Year { get; set; }
}
