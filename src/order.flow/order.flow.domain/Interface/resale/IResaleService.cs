using order.flow.domain.entity.resale;
using order.flow.domain.Interface.Base;
using order.flow.domain.repository.Interface.resale;

namespace order.flow.domain.Interface.resale;

public interface IResaleService : IServiceBase<ResaleEntity, IResaleRepository>
{
    Task<List<ResaleEntity>> GetAll();
    Task<ResaleEntity> Post(ResaleEntity model);
}