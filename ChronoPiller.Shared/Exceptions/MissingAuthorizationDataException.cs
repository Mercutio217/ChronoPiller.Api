using System;

namespace ChronoPiller.Shared.Exceptions;

public class MissingAuthorizationDataException() : Exception(CreateMessage())
{
    private static string CreateMessage() => 
        $"Mandatory fields for establishing secure connection are missing, either token data or secret.";
}