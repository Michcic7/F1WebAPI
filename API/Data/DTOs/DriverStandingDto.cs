using API.Data.Models;

namespace API.Data.DTOs;

public class DriverStandingDto
{
    public int Position { get; set; }
    public string DriverName { get; set; }
    public string Nationality { get; set; }
    public string TeamName { get; set; }
    public float Points { get; set; }
    public int? Year { get; set; }
}
