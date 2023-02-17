using API.Data.Models;

namespace API.Data.DTOs;

public class RaceDto
{
	public required string Name { get; set; }
	public required string Date { get; set; }
	public required string WinnerName { get; set; }
	public required string Car { get; set; }
	public required int Laps { get; set; }
	public required string Time { get; set; }
}