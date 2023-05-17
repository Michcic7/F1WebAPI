using API.Data.DTOs.DTOsWithMetadata;
using API.Data.DTOs;

namespace API.Interfaces;

public interface ITeamService
{
    Task<PaginatedTeamsDto> GetTeams(
        int page, int pageSize, int maxPageSize, string nameFilter, HttpContext context);
    
    Task<TeamDto> GetTeamById(int id, HttpContext context);

    Task<IEnumerable<TeamStandingDto>> GetTeamStanding(
        int year, HttpContext context);

    Task<IEnumerable<TeamStandingDto>> GetTeamAllStandingsByTeamId(
        int id, HttpContext context);

    Task<IEnumerable<RaceResultDto>> GetTeamRaceResultsByYear(
        int id, int year, HttpContext context);
}