using order.flow.application.Interface.Base;
using order.flow.domain.entity.resale;
using order.flow.domain.Interface.resale;
using order.flow.domain.repository.Interface.resale;

namespace order.flow.application.Interface.resale;

public interface IResaleAppService: IAppServiceBase<ResaleEntity, IResaleService, IResaleRepository>
{
    Task<List<ResaleEntity>> Get();
    Task<ResaleEntity> Post(ResaleEntity map);
}