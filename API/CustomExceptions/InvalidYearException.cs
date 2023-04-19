using API.Data.Models;
using API.Interfaces;

namespace API.CustomExceptions;

public class InvalidYearException : Exception, IExceptionProperties
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Detail { get; }
    public string Instance { get; }

    public InvalidYearException(Type type, string instance)
    {
        Type = "https://httpstatuses.io/400";
        Title = "Invalid year.";
        Status = StatusCodes.Status400BadRequest;
        Detail = type.Name switch
        {
            nameof(TeamStanding) =>
                "The requested year must be between 1958 and 2022.",
            nameof(DriverStanding) =>
                "The requested year must be between 1950 and 2022.",
            nameof(RaceResult) => 
                "The requested year must be between 1950 and 2022."

        };
        Instance = instance;
    }
}
