using System;

namespace ChronoPiller.Shared.Exceptions;

public class AuthorizationException : Exception
{
    public AuthorizationException() : base("You're not authorized!") { }

    public AuthorizationException(Exception innerException) : base("You're not authorized!", innerException) { }
}