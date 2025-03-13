using order.flow.application.Interface.Base;
using order.flow.domain.entity.order;
using order.flow.domain.Interface.order;
using order.flow.domain.repository.Interface.order;

namespace order.flow.application.Interface.order;

public interface IOrderAppService: IAppServiceBase<OrderEntity, IOrderService, IOrderRepository>
{
    Task<OrderEntity> Post(OrderEntity map);
}
