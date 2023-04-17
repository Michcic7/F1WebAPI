using API.Interfaces;

namespace API.CustomExceptions;

public class InvalidDriverStandingYearException : Exception, IExceptionProperties
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Detail { get; }
    public string Instance { get; }

    public InvalidDriverStandingYearException(string instance)
    {
        Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1";
        Title = "Invalid year.";
        Status = StatusCodes.Status400BadRequest;
        Detail = "The requested year must be between 1950 and 2022.";
        Instance = instance;
    }
}
