using order.flow.domain.entity.phone;
using order.flow.domain.Interface.Base;
using order.flow.domain.repository.Interface.phone;

namespace order.flow.domain.Interface.phone;

public interface IPhoneService : IServiceBase<PhoneEntity, IPhoneRepository>
{
    Task<List<PhoneEntity>> GetByResaleId(string resaleId);
}