namespace API.Data.DTOs.DTOsWithMetadata;

public record PaginatedTeamsDto
{
    public required int TotalTeams { get; init; }
    public required int TotalPages { get; init; }
    public required int CurrentPage { get; init; }
    public required int PageSize { get; init; }
    public required string? NameFilter { get; init; }
    public required IEnumerable<TeamDto> Teams { get; init; }
}
