using API.Interfaces;

namespace API.CustomExceptions.AuthExceptions;

public class ExpiredRefreshTokenException : Exception, IExceptionProperties
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Detail { get; }
    public string Instance { get; }

    public ExpiredRefreshTokenException(string instance)
    {
        Type = "https://httpstatuses.io/401";
        Title = "Invalid refresh token.";
        Status = StatusCodes.Status401Unauthorized;
        Detail = "Your refresh token has expired. Please use the login endpoint to renew it.";
        Instance = instance;
    }
}
