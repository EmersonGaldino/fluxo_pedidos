using Dapper;
using order.flow.domain.entity.resale;
using order.flow.domain.repository.Interface.resale;
using order.flow.persistence.repositories.Base;
using order.flow.utils.shared;

namespace order.flow.persistence.repositories.resale;

public class ResaleRepository(IConnectionPostgres uow): RepositoryBase<ResaleEntity>(uow), IResaleRepository
{
    #region .::Constructor
    private IConnectionPostgres Uow { get; set; } = uow;
    #endregion

    #region .::Methods

    public async Task<List<ResaleEntity>> GetAll()
    {
        var list = 
            await uow.GetConnection().QueryAsync<ResaleEntity>($"SELECT {GetFields()} FROM {GetTableName()}", uow.GetTransaction());
        return list.ToList();
    }

    public async Task<ResaleEntity> Post(ResaleEntity model) => await SaveReturnObjectAsync(model);


    #endregion

}