using Dapper;
using order.flow.domain.entity.address;
using order.flow.domain.repository.Interface.address;
using order.flow.persistence.repositories.Base;
using order.flow.utils.shared;

namespace order.flow.persistence.repositories.address;

public class AddressRepository(IConnectionPostgres uow) : RepositoryBase<AddressEntity>(uow), IAddressRepository
{
    #region .::Constructor
    private IConnectionPostgres Uow { get; set; } = uow;
    #endregion

    public async Task<IList<AddressEntity>> GetByResaleId(string resaleId)
    {
        var result = await uow.GetConnection().QueryAsync<AddressEntity>(
            $"SELECT {GetFields()} FROM {GetTableName()} WHERE resale_id = '{resaleId}'", uow.GetTransaction());
        return result.ToList();
    }
}