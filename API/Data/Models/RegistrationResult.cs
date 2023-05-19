namespace API.Data.Models;

public record RegistrationResult
{
    public string Message => "Registration succesful";
    public required string Username { get; init; }
}
