using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace API.ProblemDetailsErrors;

public class YearProblemDetails<T> : ProblemDetails
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Detail { get; }
    public string Instance { get; }

    public YearProblemDetails(YearProblemDetailsType type, HttpContext context)
    {
        (string detail, string type) details = type switch
        {
            YearProblemDetailsType.DriverStanding =>
                ("Drivers Championships took place between 1950 and 2022.",
                 "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1"),

            YearProblemDetailsType.TeamStanding =>
                ("Constructors Championships took place betwwen 1958 and 2022.",
                 "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1")
        };

        Type = details.type;
        Title = "Invalid year.";
        Status = (int)StatusCodes.Status400BadRequest;
        Detail = details.detail;
        //Instance = $"/{typeof(T).Name.GetTypeOfStanding().ToLower()}s/{typeof(T).Name.ToLower()}";
        Instance = $"{context.Request.Path}{context.Request.QueryString}";
    }

    
}

public enum YearProblemDetailsType
{
    DriverStanding,
    TeamStanding
}
