using order.flow.domain.generic.Interface.uow;

namespace order.flow.domain.generic;

public interface IGetUow
{
    IUnitOfWork GetUow();
}