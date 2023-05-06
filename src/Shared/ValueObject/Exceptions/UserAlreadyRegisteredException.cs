namespace Shared.ValueObject.Exceptions;

public class UserAlreadyRegisteredException : DashDriverException
{
    public UserAlreadyRegisteredException(string message) : base(message) { }
}
