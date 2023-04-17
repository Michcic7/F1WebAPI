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
        Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.4";
        Title = "Standings not found.";
        Status = StatusCodes.Status404NotFound;
        Detail = "Could not find standings for a driver with the given ID.";
        Instance = instance;
    }
}
