using order.flow.domain.entity.resale;
using order.flow.domain.Interface.address;
using order.flow.domain.Interface.phone;
using order.flow.domain.Interface.resale;
using order.flow.domain.repository.Interface.resale;
using order.flow.domain.service.Base;

namespace order.flow.domain.service.resale;

public class ResaleService(
    IResaleRepository repository,
    IPhoneService phoneService,
    IAddressService addressService)
    : ServiceBase<ResaleEntity, IResaleRepository>(repository), IResaleService
{
    public async Task<List<ResaleEntity>> GetAll()
    {
       
        var results =await repository.GetAll();
        foreach (var item in results)
        {
            item.Phones = await phoneService.GetByResaleId(item.Id.ToString());
            item.AddressDelevery = await addressService.GetByResaleId(item.Id.ToString());
        }
        return results;
    }

    public async Task<ResaleEntity> Post(ResaleEntity model)
    {
        return await repository.Post(model);
    }
}
