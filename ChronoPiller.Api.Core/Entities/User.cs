using System.Net.Http.Headers;

namespace ChronoPiller.Api.Core.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Prescription> Prescriptions { get; set; }
}