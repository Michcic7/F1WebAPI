namespace ScrapperConsoleApp.Models;

public class Race
{
	public int RaceId { get; set; }
	public string Name { get; set; }
	public string Date { get; set; }
	public string WinnerName { get; set; }
	public string Car { get; set; }
	public int Laps { get; set; }
	public string Time { get; set; }

	public int StandingsYearId { get; set; }
}