using API.Data.DTOs;

namespace API.Data.Models;

public class AuthenticationResult
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expiry { get; set; }
}
