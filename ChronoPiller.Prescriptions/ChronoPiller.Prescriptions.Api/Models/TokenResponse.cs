namespace ChronoPiller.Api.Models;

public class TokenResponse
{
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public IEnumerable<string> Roles { get; set; }
    public UserResponse UserData { get; set; } 
}