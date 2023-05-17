namespace API.Interfaces;

public interface IExceptionProperties
{
    string Type { get; }
    string Title { get; }
    int Status { get; }
    string Detail { get; }
    string Instance { get; }
}
