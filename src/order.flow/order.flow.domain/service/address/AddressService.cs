using order.flow.domain.entity.address;
using order.flow.domain.Interface.address;
using order.flow.domain.repository.Interface.address;
using order.flow.domain.service.Base;

namespace order.flow.domain.service.address;

public class AddressService(IAddressRepository repository)
:ServiceBase<AddressEntity, IAddressRepository>(repository), IAddressService
{
    public async Task<IList<AddressEntity>> GetByResaleId(string resaleId) => await repository.GetByResaleId(resaleId);

}