namespace Shared.ValueObject.Exceptions;

public class ValidationErrorException : DashDriverException
{
    
    public ValidationErrorException(IEnumerable<string> errorMessages) : base(errorMessages)
    {
    }
}
