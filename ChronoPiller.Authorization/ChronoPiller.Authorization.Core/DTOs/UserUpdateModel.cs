namespace ChronoPiller.Authorization.Core.DTOs;

public class UserUpdateModel
{
    public int Id { get;  set; }
    public string? UserName { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}