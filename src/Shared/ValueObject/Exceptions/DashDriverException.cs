namespace Shared.ValueObject.Exceptions;

public class DashDriverException : SystemException
{
    public IEnumerable<string> ErrorMessages { get; set; }
    public DashDriverException(string message) : base(message) { }

    public DashDriverException(IEnumerable<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}
