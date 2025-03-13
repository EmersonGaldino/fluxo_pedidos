using order.flow.domain.entity.order;
using order.flow.domain.Interface.Base;
using order.flow.domain.repository.Interface.order;

namespace order.flow.domain.Interface.order;

public interface IOrderService: IServiceBase<OrderEntity, IOrderRepository>
{
    Task<OrderEntity> Post(OrderEntity model);
    Task UpdateStatusAsync(Guid orderOrderId);
}
