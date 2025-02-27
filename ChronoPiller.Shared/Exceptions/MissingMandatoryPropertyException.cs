﻿namespace ChronoPiller.Shared.Exceptions;

public class MissingMandatoryPropertyException<T> : ChronoValidationException
{
    private string _propertyName;
    
    public MissingMandatoryPropertyException(string propertyName) 
        : base($"Missing property mandatory property {propertyName} of type {typeof(T).Name}")
    {
        _propertyName = propertyName;
    }
    public override string GetValidationErrorMessage() => 
        $"Property {_propertyName} for type {typeof(T).Name} is mandatory!";
}