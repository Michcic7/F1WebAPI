namespace API.CustomExceptions;

public class DriverNotFoundException : Exception
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Detail { get; }
    public string Instance { get; }

    public DriverNotFoundException(string instance, int driverId)
    {
        Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.4";
        Title = "Driver not found.";
        Status = StatusCodes.Status404NotFound;
        Detail = $"Driver with ID = {driverId} was not found.";
        Instance = instance;
    }
}
