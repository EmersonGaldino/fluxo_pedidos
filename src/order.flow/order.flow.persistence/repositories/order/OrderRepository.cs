using Dapper;
using order.flow.domain.entity.order;
using order.flow.domain.repository.Interface.order;
using order.flow.persistence.repositories.Base;
using order.flow.utils.shared;

namespace order.flow.persistence.repositories.order;

public class OrderRepository(IConnectionPostgres uow) : RepositoryBase<OrderEntity>(uow), IOrderRepository
{
    #region .::Constructor
    private IConnectionPostgres Uow { get; set; } = uow;

    #endregion

    public async Task<OrderEntity> Post(OrderEntity model) => await SaveReturnObjectAsync(model);

    public async Task UpdateSatatusAsync(Guid orderId) =>
        await uow.GetConnection().ExecuteAsync($"UPDATE {GetTableName()} SET status='E' WHERE id = '{orderId}'", uow.GetTransaction());

}