namespace ChronoPiller.Api.Core.Exceptions;

public class AuthenticationException : Exception
{
    public AuthenticationException() : base("You're not authenticated") { }

    public AuthenticationException(Exception innerException) : base("You're not authenticated!", innerException) { }
}