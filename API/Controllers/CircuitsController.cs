using API.Data.DTOs;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class CircuitsController : ControllerBase
{
    private readonly ICircuitService _service;

    public CircuitsController(ICircuitService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<CircuitDto>> GetCircuits()
    {
        IEnumerable<CircuitDto> circuits = await _service.GetCircuits();

        return Ok(circuits);
    }
}
