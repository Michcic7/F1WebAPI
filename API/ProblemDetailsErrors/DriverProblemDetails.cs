using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.ProblemDetailsErrors;

public class DriverProblemDetails : ProblemDetails
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Detail { get; }
    public string Instance { get; }

    public DriverProblemDetails(string id)
    {
        Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.4";
        Title = "Invalid ID.";
        Status = (int) HttpStatusCode.NotFound;
        Detail = "There is no driver with such an ID.";
        Instance = $"/drivers/{id}";
    }
}
