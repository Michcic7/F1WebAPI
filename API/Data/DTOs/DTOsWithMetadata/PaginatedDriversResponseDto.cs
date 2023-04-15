namespace API.Data.DTOs.DTOsWithMetadata;

public class PaginatedDriversDto
{
    public int TotalDrivers { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public string NameFilter { get; set; }
    public IEnumerable<DriverDto> Drivers { get; set; }
}
