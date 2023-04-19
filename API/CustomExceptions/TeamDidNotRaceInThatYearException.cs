using API.Interfaces;

namespace API.CustomExceptions;

public class TeamDidNotRaceInThatYearException : Exception, IExceptionProperties
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Detail { get; }
    public string Instance { get; }

    public TeamDidNotRaceInThatYearException(string instance)
    {
        Type = "https://httpstatuses.io/404";
        Title = "Race results not found.";
        Status = StatusCodes.Status404NotFound;
        Detail = "Could not find race results for the given team in the given year.";
        Instance = instance;
    }
}
