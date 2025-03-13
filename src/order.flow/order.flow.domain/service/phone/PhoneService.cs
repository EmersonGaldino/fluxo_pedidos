using order.flow.domain.entity.phone;
using order.flow.domain.Interface.phone;
using order.flow.domain.repository.Interface.phone;
using order.flow.domain.service.Base;

namespace order.flow.domain.service.phone;

public class PhoneService(IPhoneRepository repository)
    : ServiceBase<PhoneEntity, IPhoneRepository>(repository), IPhoneService
{
    public async Task<List<PhoneEntity>> GetByResaleId(string resaleId) => await repository.GetByResaleId(resaleId);
}