using API.Interfaces;

namespace API.CustomExceptions;

public class NonPositivePageNumberException : Exception, IExceptionProperties
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Detail { get; }
    public string Instance { get; }

    public NonPositivePageNumberException(string instance)
    {
        Type = "https://httpstatuses.io/400";
        Title = "Invalid page number.";
        Status = StatusCodes.Status400BadRequest;
        Detail = "Requested page number must be greater than 0.";
        Instance = instance;
    }
}
