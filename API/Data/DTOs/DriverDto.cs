namespace API.Data.DTOs;

public class DriverDto
{
	public required string Position { get; set; }
	public required string Name { get; set; }
	public required string Nationality { get; set; }
	public required string Team { get; set; }
	public required float Points { get; set; }
}
