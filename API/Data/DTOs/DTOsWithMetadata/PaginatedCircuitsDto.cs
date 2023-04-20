namespace API.Data.DTOs.DTOsWithMetadata;

public class PaginatedCircuitsDto
{
    public int TotalCircuits { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public string NameFilter { get; set; }
    public IEnumerable<CircuitDto> Circuits { get; set; }
}
