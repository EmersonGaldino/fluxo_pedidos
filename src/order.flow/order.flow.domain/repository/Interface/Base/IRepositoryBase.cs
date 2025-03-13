using order.flow.domain.generic;
using order.flow.domain.generic.Interface;

namespace order.flow.domain.repository.Interface.Base;

public interface IRepositoryBase<T> : IGet<T>,
    ISelect<T>, IListByIds<T>, IGetUow, ISave<T>, ISaveAll<T> where T : class
{
    
}