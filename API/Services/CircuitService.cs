using API.Data;
using API.Data.DTOs;
using API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface ICircuitService
{
    Task<IEnumerable<CircuitDto>> GetCircuits();
    Task<CircuitDto> GetCircuitById(int id);
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

    public async Task<CircuitDto> GetCircuitById(int id)
    {
        Circuit circuit = await _context.Circuits.FindAsync(id);

        if (circuit != null)
        {
            return new CircuitDto
            {
                CircuitId = circuit.CircuitId,
                Name = circuit.Name,
                Location = circuit.Location
            };
        }
        else
        {
            return null;
        }
    }
}
