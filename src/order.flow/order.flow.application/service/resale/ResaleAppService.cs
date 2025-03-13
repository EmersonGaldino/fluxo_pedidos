using order.flow.application.Interface.Base;
using order.flow.application.Interface.resale;
using order.flow.application.service.Base;
using order.flow.domain.entity.resale;
using order.flow.domain.Interface.resale;
using order.flow.domain.repository.Interface.resale;

namespace order.flow.application.service.resale;

public class ResaleAppService(IResaleService service)
    : AppServiceBase<ResaleEntity,IResaleService,IResaleRepository>(service), IResaleAppService
{
    #region .::Methods

    public async Task<List<ResaleEntity>> Get() => await service.GetAll();
    public async Task<ResaleEntity> Post(ResaleEntity map) => await service.Post(map);


    #endregion

}
