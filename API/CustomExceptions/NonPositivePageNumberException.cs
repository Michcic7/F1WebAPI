namespace API.CustomExceptions;

public class NonPositivePageNumberException : Exception
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Detail { get; }
    public string Instance { get; }

    public NonPositivePageNumberException(string instance)
    {
        Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1";
        Title = "Invalid page number.";
        Status = StatusCodes.Status400BadRequest;
        Detail = "Requested page number must be greater than 0.";
        Instance = instance;
    }
}
