using System;
using ChronoPiller.Shared.Enums;

namespace ChronoPiller.Shared.Authorization;

public class ChronoTokenData : ChronoUserResponse
{
    public Roles Role { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
}