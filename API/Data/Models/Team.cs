namespace API.Data.Models;

public class Team
{
    public int TeamId { get; set; }
    public string Name { get; set; }

    public List<TeamStanding> TeamStandings { get; set; }
    public List<RaceResult> RaceResults { get; set; }
}
