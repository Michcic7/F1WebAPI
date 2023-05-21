namespace API.Data.DTOs.DTOsWithMetadata;

public record PaginatedCircuitsDto
{
    public required int TotalCircuits { get; init; }
    public required int TotalPages { get; init; }
    public required int CurrentPage { get; init; }
    public required int PageSize { get; init; }
    public required string? NameFilter { get; init; }
    public required IEnumerable<CircuitDto> Circuits { get; init; }
}
