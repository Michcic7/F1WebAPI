using API.Interfaces;

namespace API.CustomExceptions.AuthExceptions;

public class InvalidUserCredentialException : Exception, IExceptionProperties
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Detail { get; }
    public string Instance { get; }

    public InvalidUserCredentialException(string instance)
    {
        Type = "https://httpstatuses.io/401";
        Title = "Invalid credential.";
        Status = StatusCodes.Status401Unauthorized;
        Detail = "The username or password is wrong.";
        Instance = instance;
    }
}
