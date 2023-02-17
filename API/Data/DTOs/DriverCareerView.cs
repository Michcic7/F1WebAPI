using API.Data.Models;

namespace API.Data.DTOs;

public class DriverCareerDto
{
	public required string Name { get; set; }
	public required string Nationality { get; set; }
	public required int RaceWins { get; set; }
	public required int ChampionshipWins { get; set; }
}
