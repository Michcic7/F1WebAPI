using API.Data.DTOs;
using API.Data.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class CircuitsController : ControllerBase
{
    private const int maxPageSize = 40;

    private readonly ICircuitService _circuitService;
    private readonly IRaceResultService _raceResultService;

    public CircuitsController(
        ICircuitService service,
        IRaceResultService raceResultService)
    {
        _circuitService = service;
        _raceResultService = raceResultService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CircuitDto>>> GetCircuits(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string name = null)
    {
        IEnumerable<CircuitDto> circuits = await _circuitService.GetCircuits();

        if (pageSize <= 0 || page <= 0)
        {
            return BadRequest();
        }
        
        if (!string.IsNullOrEmpty(name))
        {
            circuits = circuits.Where(c =>
                c.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase) ||
                c.Name.EndsWith(name, StringComparison.OrdinalIgnoreCase));
        }

        int totalCircuits = circuits.Count();
        int totalPages = (int)Math.Ceiling((double)totalCircuits / pageSize);

        if (page > totalPages)
        {
            return BadRequest();
        }

        IEnumerable<CircuitDto> paginatedcircuits = circuits.Skip((page - 1) * pageSize).Take(pageSize);

        var response = new
        {
            TotalCircuits = totalCircuits,
            TotalPages = totalPages,
            CurrentPage = page,
            PageSize = pageSize,
            NameFilter = name,
            Circuits = paginatedcircuits
        };

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CircuitDto>> GetCircuitById(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }
        
        CircuitDto circuit = await _circuitService.GetCircuitById(id);

        if (circuit is null)
        {
            return NotFound();
        }

        return Ok(circuit);
    }

    [HttpGet]
    [Route("{id}/raceresults")]
    public async Task<ActionResult<IEnumerable<RaceResultDto>>> GetCircuitRaceResultsByYear(
        int id, [FromQuery] int year = 2022)
    {
        if (id <= 0 || year < 1950 || year > 2022)
        {
            return BadRequest();
        }

        IEnumerable<RaceResultDto> raceResults = await
            _raceResultService.GetCircuitRaceResultsByYear(id, year);

        if (raceResults is null)
        {
            return NotFound();
        }

        return Ok(raceResults);
    }
}
