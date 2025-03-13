using order.flow.domain.entity.order;
using order.flow.domain.repository.Interface.Base;

namespace order.flow.domain.repository.Interface.order;

public interface IOrderItemRepository : IRepositoryBase<OrderItemEntity>
{
    Task<OrderItemEntity> Post(OrderItemEntity modelItems);
    Task<List<OrderCountEntity>> GetAllCountAsync();
}