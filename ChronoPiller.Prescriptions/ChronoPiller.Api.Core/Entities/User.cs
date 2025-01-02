using System.Net.Http.Headers;
using ChronoPiller.Api.Core.DTOs;
using ChronoPiller.Api.Core.Exceptions;

namespace ChronoPiller.Api.Core.Entities;

public class User : BaseEntity
{
    private User(string email, string username, string name, string surname)
    {
        Email = email;
        UserName = username;
        Name = name;
        Surname = surname;    
    }
    
    private User() { }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public List<Role> Roles { get; set; }
    public List<Prescription> Prescriptions { get; set; }

    public void Update(UserUpdateModel model)
    {
        if(model.Name is not null)
            Name = model.Name;
        if(model.UserName is not null)
            UserName = model.UserName;
        if(model.Surname is not null)
            Surname = model.Surname;
    }
    
    public static User? Create(string email, string username, string name, string surname)
    {
        if (username is null)
            throw new MissingMandatoryPropertyException<User>(nameof(Name));

        if (username.Length > 50)
            throw new InvalidLenghtOfPropertyException(nameof(UserName), username);
        
        if (name is null)
            throw new MissingMandatoryPropertyException<User>(nameof(Name));
        
        if (name.Length > 50)
            throw new InvalidLenghtOfPropertyException(nameof(Name), username);
        
        if (surname is null)
            throw new MissingMandatoryPropertyException<User>(nameof(Name));
        
        if (surname.Length > 50)
            throw new InvalidLenghtOfPropertyException(nameof(Surname), username);
        
        return new User(email, username, name, surname);
    }
}