using API.Data.DTOs.DTOsWithMetadata;
using API.Data.DTOs;

namespace API.Interfaces;

public interface IDriverService
{
    Task<PaginatedDriversDto> GetDrivers(
        int page, int pageSize, int maxPageSize, string nameFilter, 
        string nationalityFilter, HttpContext context);

    Task<DriverDto> GetDriverById(int id, HttpContext context);

    Task<IEnumerable<DriverStandingDto>> GetDriverStanding(
        int year, HttpContext context);

    Task<IEnumerable<DriverStandingDto>> GetDriverAllStandingsByDriverId(
        int id, HttpContext context);

    Task<IEnumerable<RaceResultDto>> GetDriverRaceResultsByYear(
        int id, int year, HttpContext context);
}