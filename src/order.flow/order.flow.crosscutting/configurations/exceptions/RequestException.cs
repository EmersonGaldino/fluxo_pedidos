namespace order.flow.crosscutting.configurations.exceptions;

public class RequestException : Exception
{
    public string ErrorMessage { get; set; }
    public int StatusCode { get;  }
    public RequestException(int statussCode, string message)
    {
        StatusCode = statussCode;
        ErrorMessage = message;
    }
}