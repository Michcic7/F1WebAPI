using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data.Models;

public class StandingsYear
{
    public int StandingsYearId { get; set; }
    public List<Driver> Drivers { get; set; }
    public List<Team> Teams { get; set; }
    public List<Race> Races { get; set; }
}
