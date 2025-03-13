using order.flow.application.Interface.Base;
using order.flow.domain.Interface.Base;
using order.flow.domain.repository.Interface.Base;

namespace order.flow.application.service.Base;

public class AppServiceBase<T, S, R> : IAppServiceBase<T, S, R>
    where T : class
    where S : IServiceBase<T, R>
    where R : IRepositoryBase<T>
{
    protected readonly IServiceBase<T, R> _service;

    public AppServiceBase(IServiceBase<T, R> serviceBase)
    {
        _service = serviceBase;
    }

    public S GetService() => (S)_service;
}