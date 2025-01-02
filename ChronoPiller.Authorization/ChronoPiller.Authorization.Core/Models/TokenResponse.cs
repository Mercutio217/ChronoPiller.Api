using System;

namespace ChronoPiller.Authorization.Core.Models;

public class TokenResponse : UserResponse
{
    public string Role { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
}