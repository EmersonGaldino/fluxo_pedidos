namespace order.flow.utils.helpers;

public class RequestException : Exception
{
    public RequestException(int statusCode, string message)
    {
        StatusCode = statusCode;
        ErrorMessage = message;
    }
    public string ErrorMessage { get; set; }
    public int StatusCode { get; }
}