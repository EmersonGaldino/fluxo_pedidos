using order.flow.domain.entity.address;
using order.flow.domain.Interface.Base;
using order.flow.domain.repository.Interface.address;

namespace order.flow.domain.Interface.address;

public interface IAddressService: IServiceBase<AddressEntity, IAddressRepository>
{
    Task<IList<AddressEntity>> GetByResaleId(string toString);
}