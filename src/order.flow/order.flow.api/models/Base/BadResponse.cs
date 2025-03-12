namespace order.flow.api.models.Base;

public sealed class BadResponse : BaseResponse
{
    public BadResponse(string error): base(false)
    {
        Errors = new List<string> { error };
    }

    public BadResponse(List<string> errors) : base(false)
    {
        Errors = errors;
    }

    public List<string> Errors { get; private set; }
}