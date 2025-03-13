namespace order.flow.api.models.Base;

public class BaseModelView<T> where T : new()
{
    public bool Success { get; set; } = true;

    public string Message { get; set; } = null;

    public long TimerProcessing { get; set; } = 0;

    public T Data { get; set; } = new T();

    public void GenerateError(T item, string message)
    {
        Message = message;
        Success = false;
    }
}