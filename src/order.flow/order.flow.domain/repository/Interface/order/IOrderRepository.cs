using order.flow.domain.entity.order;
using order.flow.domain.repository.Interface.Base;

namespace order.flow.domain.repository.Interface.order;

public interface IOrderRepository : IRepositoryBase<OrderEntity>
{
    Task<OrderEntity> Post(OrderEntity model);
    Task UpdateSatatusAsync(Guid orderId);
}

