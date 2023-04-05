using API.Data.DTOs;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class CircuitsController : ControllerBase
{
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
    public async Task<ActionResult<CircuitDto>> GetCircuits()
    {
        IEnumerable<CircuitDto> circuits = await _circuitService.GetCircuits();

        return Ok(circuits);
    }

    [HttpGet]
    [Route("{circuitId}/{year}/raceresults")]
    public async Task<ActionResult<RaceResultDto>> GetCircuitRaceResultsByYear(int circuitId, int year)
    {
        IEnumerable<RaceResultDto> raceResults = await
            _raceResultService.GetCircuitRaceResultsByYear(circuitId, year);

        return Ok(raceResults);
    }
}
