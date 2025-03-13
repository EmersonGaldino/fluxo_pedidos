using order.flow.application.Interface.phone;
using order.flow.application.service.Base;
using order.flow.domain.entity.phone;
using order.flow.domain.Interface.phone;
using order.flow.domain.repository.Interface.phone;

namespace order.flow.application.service.phone;

public class PhoneAppService(IPhoneService service) 
    : AppServiceBase<PhoneEntity, IPhoneService,IPhoneRepository>(service), IPhoneAppService
{
    
}