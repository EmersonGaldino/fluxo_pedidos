namespace order.flow.api.models.Base;

public class BaseResponse
{
    public bool Success { get; private set; }
    protected BaseResponse(bool success)
    {
        Success = success;
    }
}