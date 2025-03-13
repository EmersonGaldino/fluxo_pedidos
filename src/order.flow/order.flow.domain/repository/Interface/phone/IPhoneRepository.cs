using order.flow.domain.entity.phone;
using order.flow.domain.repository.Interface.Base;

namespace order.flow.domain.repository.Interface.phone;

public interface IPhoneRepository : IRepositoryBase<PhoneEntity>
{
    Task<List<PhoneEntity>> GetByResaleId(string resaleId);
}