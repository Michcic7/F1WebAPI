using API.Data.DTOs;
using API.Data.DTOs.DTOsWithMetadata;
using API.ProblemDetailsErrors;
using API.HelperMethods;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using API.Data.Models;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class DriversController : ControllerBase
{
    private const int _maxPageSize = 40;

    private readonly IDriverService _driverService;
    private readonly IDriverStandingService _driverStandingService;
    private readonly IRaceResultService _raceResultService;

    public DriversController(
        IDriverService service, 
        IDriverStandingService driverStandingService, 
        IRaceResultService raceResultService)
    {
        _driverService = service;
        _driverStandingService = driverStandingService;
        _raceResultService = raceResultService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DriverDto>>> GetDrivers(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string name = null)
    {
        //if (page <= 0)
        //{
        //    ProblemDetailsHelper.SetPageNumberError(
        //        HttpContext, PageNumberErrorType.ZeroOrNegativePageNumber);
        //    return BadRequest();
        //}

        IEnumerable<DriverDto> drivers = await _driverService.GetDrivers(page, pageSize, name);

        //if (!string.IsNullOrEmpty(name))
        //{
        //    drivers = drivers.Where(d =>
        //        d.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase) ||
        //        d.Name.EndsWith(name, StringComparison.OrdinalIgnoreCase));
        //}

        CalculateMetadata(pageSize, drivers, out int totalDrivers, out int totalPages);

        ActionResult validationSelectedPageResult = ValidatePageInput(page, totalPages);
        
        if (validationSelectedPageResult != null)
        {
            return validationSelectedPageResult;
        }

        IEnumerable<DriverDto> paginatedDrivers = PaginateDrivers(page, pageSize, drivers);

        PaginatedDriversResponseDto response = new()
        {
            TotalDrivers = totalDrivers,
            TotalPages = totalPages,
            CurrentPage = page,
            PageSize = pageSize,
            NameFilter = name,
            Drivers = paginatedDrivers
        };

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DriverDto>> GetDriverById(int id)
    {
        DriverDto driver = await _driverService.GetDriverById(id, HttpContext);

        return Ok(driver);
    }

    //[HttpGet("{id}")]
    //public async Task<ActionResult<DriverDto>> GetDriverById(int id)
    //{
    //    if (id <= 0)
    //    {
    //        EntityProblemDetails<Driver> problemDetails = new(
    //            id.ToString(), EntityProblemDetailsType.NonPositiveId, HttpContext);

    //        return BadRequest(problemDetails);
    //    }

    //    DriverDto driver = await _driverService.GetDriverById(id);

    //    if (driver == null)
    //    {
    //        EntityProblemDetails<Driver> problemDetails = new(
    //            id.ToString(), EntityProblemDetailsType.NotFoundId, HttpContext);

    //        return NotFound(problemDetails);
    //    }

    //    return Ok(driver);
    //}

    [HttpGet("DriverStanding")]
    public async Task<ActionResult<IEnumerable<DriverStandingDto>>> GetDriverStanding(
        [FromQuery] int year = 2022)
    {
        if (year < 1950 || year > 2022)
        {
            YearProblemDetails<DriverStanding> problemDetails = new(
                YearProblemDetailsType.DriverStanding, HttpContext);

            return BadRequest(problemDetails);
        }
        
        IEnumerable<DriverStandingDto> driverStandings = await 
            _driverStandingService.GetDriverStanding(year);

        return Ok(driverStandings);
    }

    [HttpGet("{id}/DriverStandings")]
    public async Task<ActionResult<IEnumerable<DriverStandingDto>>> GetDriverAllStandingsById(
        int id)
    {
        if (id <= 0)
        {
            EntityProblemDetails<Driver> problemDetails = new(
                id.ToString(), EntityProblemDetailsType.NonPositiveId, HttpContext);

            return BadRequest(problemDetails);
        }

        IEnumerable<DriverStandingDto> driverStandings = await
            _driverStandingService.GetDriverAllStandingsById(id);

        if (driverStandings is null)
        {
            ProblemDetailsHelper.SetStandingsMissingError(HttpContext, StandingType.DriverStanding);
            return NotFound();
        }

        return Ok(driverStandings);
    }

    [HttpGet("{id}/RaceResults")]
    public async Task<ActionResult<IEnumerable<RaceResultDto>>> GetDriverRaceResultsByYear(
        int id, [FromQuery] int year = 2022)
    {
        if (year < 1950 || year > 2022)
        {
            return BadRequest();
        }
        
        IEnumerable<RaceResultDto> raceResults = await
            _raceResultService.GetDriverRaceResultsByYear(id, year);

        if (raceResults is null)
        {
            return NotFound();
        }

        return Ok(raceResults);
    }

    
    private IEnumerable<DriverDto> FilterDriversByName(
        string name, IEnumerable<DriverDto> drivers)
    {
        if (!string.IsNullOrEmpty(name))
        {
            drivers = drivers.Where(d =>
                d.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase) ||
                d.Name.EndsWith(name, StringComparison.OrdinalIgnoreCase));
        }

        return drivers;
    }

    private void CalculateMetadata(int pageSize, IEnumerable<DriverDto> drivers, out int totalDrivers, out int totalPages)
    {
        totalDrivers = drivers.Count();
        totalPages = (int)Math.Ceiling((double)totalDrivers / pageSize);
    }

    private IEnumerable<DriverDto> PaginateDrivers(
        int page, int pageSize, IEnumerable<DriverDto> drivers)
    {
        return drivers.Skip((page - 1) * pageSize).Take(pageSize);
    }

    private ActionResult ValidatePaginationInput(int page, int pageSize, int maxPageSize)
    {
        

        if (pageSize > maxPageSize)
        {
            ProblemDetailsHelper.SetPageSizeError(HttpContext, PageSizeErrorType.PageSizeTooBig);
            return BadRequest();
        }

        if (pageSize <= 0)
        {
            ProblemDetailsHelper.SetPageSizeError(HttpContext, PageSizeErrorType.PageSizeTooSmall);
            return BadRequest();
        }

        return null;
    }

    private ActionResult ValidatePageInput(int page, int totalPages)
    {
        if (page > totalPages)
        {
            ProblemDetailsHelper.SetPageNumberError(
                HttpContext, PageNumberErrorType.NotExistingPageNumber);
            return BadRequest();
        }

        return null;
    }

    private ActionResult ValidateDriverId(int id)
    {
        if (id <= 0)
        {
            ProblemDetailsHelper.SetDriverIdError(HttpContext, DriverIdErrorType.ZeroOrNegativeId);
            return BadRequest();
        }

        return null;
    }

    private ActionResult ValidateYear(int year)
    {
        if (year < 1950 || year > 2022)
        {
            ProblemDetailsHelper.SetStandingYearError(
                HttpContext, StandingYearErrorType.DriverStanding);
            return BadRequest();
        }

        return null;
    }
}
