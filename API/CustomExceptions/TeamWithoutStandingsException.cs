using API.Interfaces;

namespace API.CustomExceptions;

public class TeamWithoutStandingsException : Exception, IExceptionProperties
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Detail { get; }
    public string Instance { get; }

    public TeamWithoutStandingsException(string instance)
    {
        Type = "https://httpstatuses.io/404";
        Title = "Standings not found.";
        Status = StatusCodes.Status404NotFound;
        Detail = "Could not find standings for the team with the given ID.";
        Instance = instance;
    }
}
