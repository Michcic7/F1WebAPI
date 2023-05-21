namespace API.Data.DTOs.DTOsWithMetadata;

public record PaginatedDriversDto
{
    public required int TotalDrivers { get; init; }
    public required int TotalPages { get; init; }
    public required int CurrentPage { get; init; }
    public required int PageSize { get; init; }
    public required string? NameFilter { get; init; }
    public required string? NationalityFilter { get; init; }
    public required IEnumerable<DriverDto> Drivers { get; init; }
}
