namespace API.Data.DTOs.DTOsWithMetadata;

public class PaginatedTeamsDto
{
    public int TotalTeams { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public string NameFilter { get; set; }
    public IEnumerable<TeamDto> Teams { get; set; }
}
