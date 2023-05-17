namespace API.Data.DTOs;

public record CircuitDto
{
    public required int CircuitId { get; init; }
    public required string Name { get; init; }
    public required string Location { get; init; }
}
