using API.Interfaces;

namespace API.CustomExceptions.AuthExceptions;

public class UsernameTakenException : Exception, IExceptionProperties
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Detail { get; }
    public string Instance { get; }

    public UsernameTakenException(string instance)
    {
        Type = "https://httpstatuses.io/409";
        Title = "Username taken.";
        Status = StatusCodes.Status409Conflict;
        Detail = "This username is already taken.";
        Instance = instance;
    }
}
