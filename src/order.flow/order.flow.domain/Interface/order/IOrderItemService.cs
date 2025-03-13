using order.flow.domain.entity.order;
using order.flow.domain.Interface.Base;
using order.flow.domain.repository.Interface.order;

namespace order.flow.domain.Interface.order;

public interface IOrderItemService: IServiceBase<OrderItemEntity, IOrderItemRepository>
{
    Task<OrderItemEntity> Post(OrderItemEntity item);
    Task<List<OrderCountEntity>> GetAllAsyncCount();
}