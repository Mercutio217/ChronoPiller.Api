namespace ChronoPiller.Api.Core.Exceptions;

public class AuthorizationException : Exception
{
    public AuthorizationException() : base("You're not authorized!") { }

    public AuthorizationException(Exception innerException) : base("You're not authorized!", innerException) { }
}