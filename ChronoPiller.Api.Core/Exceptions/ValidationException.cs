namespace ChronoPiller.Api.Core.Exceptions;

public abstract class ValidationException : Exception
{
    public ValidationException() : base("You fucked up, pal.") { }
    public ValidationException(Exception exception) : base("You fucked up, pal.", exception) { }
}