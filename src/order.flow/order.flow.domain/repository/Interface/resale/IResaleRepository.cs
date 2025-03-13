using order.flow.domain.entity.resale;
using order.flow.domain.repository.Interface.Base;

namespace order.flow.domain.repository.Interface.resale;

public interface IResaleRepository : IRepositoryBase<ResaleEntity>
{
    Task<List<ResaleEntity>> GetAll();
    Task<ResaleEntity> Post(ResaleEntity model);
}