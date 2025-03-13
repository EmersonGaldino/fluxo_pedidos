using order.flow.domain.entity.order;
using order.flow.domain.Interface.order;
using order.flow.domain.repository.Interface.order;
using order.flow.domain.service.Base;

namespace order.flow.domain.service.order;

public class OrderItemService(IOrderItemRepository repository)
    :ServiceBase<OrderItemEntity, IOrderItemRepository>(repository), IOrderItemService
{
    public async Task<OrderItemEntity> Post(OrderItemEntity item) => await repository.Post(item);
    public async Task<List<OrderCountEntity>> GetAllAsyncCount() => await repository.GetAllCountAsync();

}