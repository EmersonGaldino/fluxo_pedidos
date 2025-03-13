using order.flow.application.Interface.address;
using order.flow.application.service.Base;
using order.flow.domain.entity.address;
using order.flow.domain.Interface.address;
using order.flow.domain.repository.Interface.address;

namespace order.flow.application.service.address;

public class AddressAppService(IAddressService service) 
    : AppServiceBase<AddressEntity, IAddressService,IAddressRepository>(service), IAddressAppService
{
    
}
