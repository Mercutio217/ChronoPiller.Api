﻿namespace ChronoPiller.Api.Models;

public class RegisterModel
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
}