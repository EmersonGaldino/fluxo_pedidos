using order.flow.domain.entity.address;
using order.flow.domain.repository.Interface.Base;

namespace order.flow.domain.repository.Interface.address;

public interface IAddressRepository : IRepositoryBase<AddressEntity>
{
    Task<IList<AddressEntity>> GetByResaleId(string resaleId);
}