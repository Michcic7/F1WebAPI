using API.Data;
using API.Data.DTOs;
using API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface ICircuitService
{
    Task<IEnumerable<CircuitDto>> GetCircuits();
}

public class CircuitService : ICircuitService
{
    private readonly F1WebAPIContext _context;

    public CircuitService(F1WebAPIContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CircuitDto>> GetCircuits()
    {
        List<Circuit> circuits = await _context.Circuits.Select(c => c).ToListAsync();

        return circuits.Select(c =>
        {
            return new CircuitDto
            {
                CircuitId = c.CircuitId,
                Name = c.Name,
                Location = c.Location
            };
        });
    }
}
