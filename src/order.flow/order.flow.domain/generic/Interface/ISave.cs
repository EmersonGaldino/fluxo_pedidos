namespace order.flow.domain.generic.Interface;

public interface ISave<T> where T : class
{
    Task SaveAsync(T entities);
}