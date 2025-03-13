using order.flow.application.Interface.Base;
using order.flow.domain.entity.address;
using order.flow.domain.Interface.address;
using order.flow.domain.repository.Interface.address;

namespace order.flow.application.Interface.address;

public interface IAddressAppService : IAppServiceBase<AddressEntity, IAddressService, IAddressRepository>
{
    
}