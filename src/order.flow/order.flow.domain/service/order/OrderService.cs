using order.flow.domain.entity.order;
using order.flow.domain.Interface.order;
using order.flow.domain.repository.Interface.order;
using order.flow.domain.service.Base;

namespace order.flow.domain.service.order;

public class OrderService(
    IOrderRepository repository, 
    IOrderItemService orderItemService)
    :ServiceBase<OrderEntity, IOrderRepository>(repository), IOrderService
{
    public async Task<OrderEntity> Post(OrderEntity model)
    {
        model.Id = Guid.NewGuid();
        model.Status = 'R';
        
        var order = await repository.Post(model);
        order.Items = await SaveItems(model.Items, model.Id);
        return order;
    }

    public async Task UpdateSatatusAsync(Guid orderId) => await repository.UpdateSatatusAsync(orderId);
    

    private async Task<List<OrderItemEntity>> SaveItems(List<OrderItemEntity> models, Guid orderId)
    {
        var listOrderItem = new List<OrderItemEntity>();
        foreach (var item in models)
        {
            item.Id = Guid.NewGuid();
            item.OrderId = orderId;
            listOrderItem.Add(await orderItemService.Post(item));
        }
        return listOrderItem;
    }
}