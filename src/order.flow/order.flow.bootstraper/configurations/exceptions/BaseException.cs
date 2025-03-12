using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace order.flow.crosscutting.configurations.exceptions;

public class BaseException : Exception
{
    public HttpStatusCode StatusCode { get; set; }
    private string message;
    private string stackTrace;

    public BaseException(HttpStatusCode statusCode, string message)
    {
        this.StatusCode = statusCode;
        this.message = message;
    }

    public BaseException(Exception ex)
    {
        this.StatusCode = HttpStatusCode.InternalServerError;
        this.message = "Falha interna no servidor!";
        this.stackTrace = ex.StackTrace;
    }

    public override string Message => this.message;
    public override string StackTrace => this.stackTrace ?? base.StackTrace;

    public override string ToString()
    {
        var obj = new
        {
            message,
            source = this.Source,
            stackTrace = this.StackTrace
        };

        return JsonSerializer.Serialize(obj, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });
    }
}