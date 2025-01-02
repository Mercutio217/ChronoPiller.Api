namespace ChronoPiller.Authorization.Core.Models;

public class UserFilterRequest
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}