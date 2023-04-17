using API.Interfaces;

namespace API.CustomExceptions;

public class DriverDidNotRaceInThatYearException : Exception, IExceptionProperties
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Detail { get; }
    public string Instance { get; }

    public DriverDidNotRaceInThatYearException(string instance)
    {
        Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.4";
        Title = "Race results not found.";
        Status = StatusCodes.Status404NotFound;
        Detail = "Could not find race results for the given driver in the given year.";
        Instance = instance;
    }
}
