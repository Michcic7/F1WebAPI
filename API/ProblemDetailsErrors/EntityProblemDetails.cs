using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.ProblemDetailsErrors;

public class EntityProblemDetails<T> : ProblemDetails
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Detail { get; }
    public string Instance { get; }

    public EntityProblemDetails(string id, EntityProblemDetailsType type, HttpContext context)
    {
        (string detail, string type, int status) details = type switch
        {
            EntityProblemDetailsType.NonPositiveId =>
                ("ID must be bigger than 0.",
                 "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1",
                 (int)HttpStatusCode.BadRequest),

            EntityProblemDetailsType.NotFoundId =>
                ($"There is no {typeof(T).Name.ToLower()} with a given ID.",
                 "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.4",
                 (int)HttpStatusCode.NotFound)
        };

        Type = details.type;
        Title = "Invalid ID.";
        Status = details.status;
        Detail = details.detail;
        Instance = $"{context.Request.Path}{context.Request.QueryString}";
    }
}

public enum EntityProblemDetailsType
{
    NonPositiveId,
    NotFoundId
}
