using order.flow.application.Interface.order;
using order.flow.application.service.Base;
using order.flow.domain.entity.order;
using order.flow.domain.Interface.order;
using order.flow.domain.repository.Interface.order;

namespace order.flow.application.service.order;

public class OrderAppService(IOrderService service) 
    : AppServiceBase<OrderEntity, IOrderService,IOrderRepository>(service), IOrderAppService
{
    public async Task<OrderEntity> Post(OrderEntity model) => await service.Post(model);

}