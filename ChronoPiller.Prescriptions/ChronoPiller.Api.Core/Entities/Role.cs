namespace ChronoPiller.Api.Core.Entities;

public class Role : BaseEntity
{
    public string Name { get; }
    
    private Role() { }

    public Role(string name)
    {
        Name = name;
    }
}