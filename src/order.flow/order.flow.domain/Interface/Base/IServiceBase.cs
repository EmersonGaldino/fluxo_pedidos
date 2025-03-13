using order.flow.domain.generic.Interface;
using order.flow.domain.repository.Interface.Base;

namespace order.flow.domain.Interface.Base;

public interface IServiceBase<T, R> : IGet<T>, ISelect<T> where T : class where R : IRepositoryBase<T>
{
    R GetRepository();
}