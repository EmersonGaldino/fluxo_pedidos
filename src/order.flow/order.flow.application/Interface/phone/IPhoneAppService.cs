using order.flow.application.Interface.Base;
using order.flow.domain.entity.phone;
using order.flow.domain.Interface.phone;
using order.flow.domain.repository.Interface.phone;

namespace order.flow.application.Interface.phone;

public interface IPhoneAppService: IAppServiceBase<PhoneEntity, IPhoneService, IPhoneRepository>
{
    
}