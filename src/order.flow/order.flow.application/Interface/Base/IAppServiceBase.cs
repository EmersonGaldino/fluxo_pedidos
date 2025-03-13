using order.flow.domain.Interface.Base;
using order.flow.domain.repository.Interface.Base;

namespace order.flow.application.Interface.Base;

public interface IAppServiceBase<T, S, R>
    where T : class
    where S : IServiceBase<T, R>
    where R : IRepositoryBase<T>
{
    S GetService();
}