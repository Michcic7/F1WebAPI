using API.Data.DTOs;
using API.Data.DTOs.DTOsWithMetadata;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CircuitsController : ControllerBase
{
    private const int _maxPageSize = 40;
    private const int _defaultYear = 2023;

    private readonly ICircuitService _circuitService;

    public CircuitsController(ICircuitService service)
    {
        _circuitService = service;
    }

    /// <summary>
    /// Get all circuits.
    /// </summary>
    /// <param name="page">The page number.</param>
    /// <param name="pageSize">How many circuits to include per page.</param>
    /// <param name="name">The name to filter circuits.</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginatedCircuitsDto>> GetCircuits(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string name = null)
    {
        PaginatedCircuitsDto circuits = await _circuitService.GetCircuits(
            page, pageSize, _maxPageSize, name, HttpContext);

        return Ok(circuits);
    }

    /// <summary>
    /// Get a circuit by the ID.
    /// </summary>
    /// <param name="id">The ID of the circuit.</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CircuitDto>> GetCircuitById(int id)
    {        
        CircuitDto circuit = await _circuitService.GetCircuitById(id, HttpContext);

        return Ok(circuit);
    }

    /// <summary>
    /// Get all race results of the given circuit from the given year.
    /// </summary>
    /// <param name="id">The ID of the circuit.</param>
    /// <param name="year">The year of races.</param>
    /// <returns></returns>
    [HttpGet("{id}/RaceResults")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<RaceResultDto>>> GetCircuitRaceResultsByYear(
        int id, [FromQuery] int year = _defaultYear)
    {
        IEnumerable<RaceResultDto> raceResults = await
            _circuitService.GetCircuitRaceResultsByYear(id, year, HttpContext);

        return Ok(raceResults);
    }
}
