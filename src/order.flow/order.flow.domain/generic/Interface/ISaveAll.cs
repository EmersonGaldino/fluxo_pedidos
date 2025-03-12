namespace order.flow.domain.generic.Interface;

public interface ISaveAll<T> where T : class
{
    Task SaveAsync(IList<T> entities);
}