using API.Interfaces;

namespace API.CustomExceptions;

public class DriverNotFoundException : Exception, IExceptionProperties
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Detail { get; }
    public string Instance { get; }

    public DriverNotFoundException(string instance, int driverId)
    {
        Type = "https://httpstatuses.io/404";
        Title = "Driver not found.";
        Status = StatusCodes.Status404NotFound;
        Detail = $"Driver with ID = {driverId} was not found.";
        Instance = instance;
    }
}
