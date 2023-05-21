using API.Interfaces;

namespace API.CustomExceptions.AuthExceptions;

public class EmptyUserCredentialsException : Exception, IExceptionProperties
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Detail { get; }
    public string Instance { get; }

    public EmptyUserCredentialsException(string instance)
    {
        Type = "https://httpstatuses.io/400";
        Title = "Invalid credentials.";
        Status = StatusCodes.Status400BadRequest;
        Detail = "The username or password is empty.";
        Instance = instance;
    }
}
