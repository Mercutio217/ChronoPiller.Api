using System;

namespace ChronoPiller.Shared.Exceptions;

public abstract class ChronoValidationException(string message) : Exception(message)
{
    public abstract string GetValidationErrorMessage();
}