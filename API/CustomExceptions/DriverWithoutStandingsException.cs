using API.Interfaces;

namespace API.CustomExceptions;

public class DriverWithoutStandingsException : Exception, IExceptionProperties
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Detail { get; }
    public string Instance { get; }

    public DriverWithoutStandingsException(string instance)
    {
        Type = "https://httpstatuses.io/404";
        Title = "Standings not found.";
        Status = StatusCodes.Status404NotFound;
        Detail = "Could not find standings for a driver with the given ID.";
        Instance = instance;
    }
}
