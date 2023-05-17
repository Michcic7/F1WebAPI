using API.Data.DTOs.DTOsWithMetadata;
using API.Data.DTOs;

namespace API.Interfaces;

public interface ICircuitService
{
    Task<PaginatedCircuitsDto> GetCircuits(
        int page, int pageSize, int maxPageSize, string nameFilter, HttpContext context);

    Task<CircuitDto> GetCircuitById(int id, HttpContext context);

    Task<IEnumerable<RaceResultDto>> GetCircuitRaceResultsByYear(
        int id, int year, HttpContext context);
}
