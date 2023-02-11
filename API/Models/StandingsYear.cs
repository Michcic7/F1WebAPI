using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class StandingsYear
{
	public int StandingsYearId { get; set; } = 0;
	public int Year { get; set; }
	[InverseProperty("StandingsYear")]
	public List<Driver> Drivers { get; set; }
	[InverseProperty("StandingsYear")]
	public List<Team> Teams { get; set; }
	[InverseProperty("StandingsYear")]
	public List<Race> Races { get; set; }
}
