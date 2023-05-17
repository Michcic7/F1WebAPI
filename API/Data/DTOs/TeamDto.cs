namespace API.Data.DTOs;

public record TeamDto
{
	public required int TeamId { get; init; }
	public required string Name { get; init; }
}
