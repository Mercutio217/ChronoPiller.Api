using System;

namespace ChronoPiller.Shared.Exceptions;

public class MissingItemException : ChronoValidationException
{
    private readonly Guid _missingId;
    
    public MissingItemException(Guid id) : base($"Requested item with id {id} is missing from db")
    {
        _missingId = id;
    }
    
    public override string GetValidationErrorMessage() => $"One of the items ({_missingId}) is missing!";
}