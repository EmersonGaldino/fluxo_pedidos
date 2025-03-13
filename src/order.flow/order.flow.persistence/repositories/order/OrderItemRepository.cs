using Dapper;
using order.flow.domain.entity.order;
using order.flow.domain.repository.Interface.order;
using order.flow.persistence.repositories.Base;
using order.flow.utils.shared;

namespace order.flow.persistence.repositories.order;

public class OrderItemRepository(IConnectionPostgres uow) : RepositoryBase<OrderItemEntity>(uow), IOrderItemRepository
{
    #region .::Constructor
    private IConnectionPostgres Uow { get; set; } = uow;

    #endregion

    public async Task<OrderItemEntity> Post(OrderItemEntity modelItems) =>
       await SaveReturnObjectAsync(modelItems);

    public async Task<List<OrderCountEntity>> GetAllCountAsync()
    {
        var count = await uow.GetConnection().QueryAsync<OrderCountEntity>($"" +
            @$"SELECT oi.orderid,SUM(oi.quantity) AS total_quantity 
            FROM tb_orderItem oi 
            INNER JOIN tb_order o ON oi.orderid = o.id 
            WHERE o.status = 'R' 
            GROUP BY oi.orderid;", uow.GetTransaction());
        return count.ToList();
        
    }
}