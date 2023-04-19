using API.Interfaces;

namespace API.CustomExceptions;

public class InvalidDriverIdException : Exception, IExceptionProperties
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Detail { get; }
    public string Instance { get; }

    public InvalidDriverIdException(string instance)
    {
        Type = "https://httpstatuses.io/400";
        Title = "Invalid ID.";
        Status = StatusCodes.Status400BadRequest;
        Detail = "Driver ID must be greater than 0.";
        Instance = instance;
    }
}
