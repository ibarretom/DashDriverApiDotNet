namespace Shared.ValueObject.Exceptions;

public class ValidationErrorException : DashDriverException
{
    public IEnumerable<string> ErrorMessages { get; set; }
    
    public ValidationErrorException(IEnumerable<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}
