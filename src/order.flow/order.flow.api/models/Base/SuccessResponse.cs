namespace order.flow.api.models.Base;

public sealed class SuccessResponse<T> : BaseResponse where T : class
{
    public SuccessResponse(T data)
        : base(true)
    {
        Data = data;
    }

    public T Data { get; private set;}
}