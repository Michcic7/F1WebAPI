namespace ScrapperConsoleApp.Models;

public class Team
{
	public int TeamId { get; set; }
	public string Position { get; set; }
	public string Name { get; set; }
	public float Points { get; set; }

	public int StandingsYearId { get; set; }
}
