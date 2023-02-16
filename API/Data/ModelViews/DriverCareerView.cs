using API.Data.Models;

namespace API.Data.ModelViews;

public class DriverCareerView
{
	public string Name { get; set; }
	public string Nationality { get; set; }
	public int RaceWins { get; set; }
	public int ChampionshipWins { get; set; }

	public DriverCareerView(string name, string nationality,
		int raceWins, int championshipWins)
	{
		Name = name;
		Nationality = nationality;
		RaceWins = raceWins;
		ChampionshipWins = championshipWins;
	}
}
