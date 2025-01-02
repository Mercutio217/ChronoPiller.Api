namespace ChronoPiller.Shared.Exceptions;

public class UserAlreadyExistsException(string email, string usernName) : ChronoValidationException(
    $"There was attempt to create new accoutn for name{usernName}, email {email}, but it already exists.")
{
    public override string GetValidationErrorMessage() => "User with these credentials already exisis!";
}