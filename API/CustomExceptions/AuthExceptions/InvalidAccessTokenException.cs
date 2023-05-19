using API.Interfaces;

namespace API.CustomExceptions.AuthExceptions;

public class InvalidAccessTokenException : Exception, IExceptionProperties
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Detail { get; }
    public string Instance { get; }

    public InvalidAccessTokenException(string instance)
    {
        Type = "https://httpstatuses.io/401";
        Title = "Invalid access token.";
        Status = StatusCodes.Status401Unauthorized;
        Detail = "The access token is malformed.";
        Instance = instance;
    }
}
