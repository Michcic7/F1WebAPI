namespace API.Data.Models;

public class DriverStanding
{
    public int DriverStandingId { get; set; }
    public int Year { get; set; }
    public int Position { get; set; }
    public int DriverId { get; set; }
    public Driver Driver { get; set; }
    public int TeamId { get; set; }
    public Team Team { get; set; }
    public float Points { get; set; }
}
