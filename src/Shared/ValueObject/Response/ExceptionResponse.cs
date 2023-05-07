namespace Shared.ValueObject.Response;

public class ExceptionResponse
{
    public List<string> Messages { get; set; }
    
    public ExceptionResponse(string message)
    {
        Messages = new List<string>()
        {
            message
        };
    }

    public ExceptionResponse(List<string> messages)
    {
        Messages = messages;
    }
}
