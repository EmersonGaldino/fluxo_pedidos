using order.flow.domain.Interface.Base;
using order.flow.domain.repository.Interface.Base;

namespace order.flow.domain.service.Base;

public class ServiceBase<T,R>
    : IServiceBase<T,R>
    where T : class
    where R : IRepositoryBase<T>
{
    #region Constructor
    protected readonly IRepositoryBase<T> repositoryBase;
    public ServiceBase(IRepositoryBase<T> repository)
    {
        repositoryBase = repository;
    }
    #endregion

    #region Methods
    public R GetRepository() => (R)repositoryBase;

    #endregion
}