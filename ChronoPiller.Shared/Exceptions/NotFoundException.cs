using System;

namespace ChronoPiller.Shared.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base("Entity not found") { }
    public NotFoundException(Exception exception) : base("Entity not found", exception) { }
}