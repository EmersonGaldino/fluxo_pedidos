using Dapper;
using order.flow.domain.entity.phone;
using order.flow.domain.repository.Interface.phone;
using order.flow.persistence.repositories.Base;
using order.flow.utils.shared;

namespace order.flow.persistence.repositories.phone;

public class PhoneRepository(IConnectionPostgres uow) : RepositoryBase<PhoneEntity>(uow), IPhoneRepository
{
    #region .::Constructor
    private IConnectionPostgres Uow { get; set; } = uow;

    #endregion

    public async Task<List<PhoneEntity>> GetByResaleId(string resaleId)
    {
        var result = await uow.GetConnection().QueryAsync<PhoneEntity>(
            $"SELECT {GetFields()} FROM {GetTableName()} WHERE resale_id = '{resaleId}'", uow.GetTransaction());
        return result.ToList();
    }
}