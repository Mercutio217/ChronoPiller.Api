namespace ChronoPiller.Api.Models;

public class UserUpdateRequest : UserRequest
{
    public Guid Id { get; private set; }
}