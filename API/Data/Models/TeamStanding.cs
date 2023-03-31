namespace API.Data.Models;

public class TeamStanding
{
    public int TeamStandingId { get; set; }
    public int Year { get; set; }
    public int Position { get; set; }
    public int TeamId { get; set; }
    public Team Team { get; set; }
    public float Points { get; set; }
}
