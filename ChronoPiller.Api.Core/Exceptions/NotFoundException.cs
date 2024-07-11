namespace ChronoPiller.Api.Core.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base("Entity not found") { }
    public NotFoundException(Exception exception) : base("Entity not found", exception) { }
}