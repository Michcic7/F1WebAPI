namespace API.Data.DTOs;

public record DriverDto
{
	public required int DriverId { get; init; }
	public required string Name { get; init; }
	public required string Nationality { get; init; }
}
