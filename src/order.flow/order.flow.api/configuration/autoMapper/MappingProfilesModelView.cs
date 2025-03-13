using AutoMapper;
using order.flow.api.models.Base;
using order.flow.api.models.order;
using order.flow.api.models.resale;
using order.flow.domain.entity.address;
using order.flow.domain.entity.Base;
using order.flow.domain.entity.order;
using order.flow.domain.entity.phone;
using order.flow.domain.entity.resale;

namespace order.flow.api.configuration.autoMapper;

public class MappingProfilesModelView : Profile
{
    public MappingProfilesModelView()
    {
        CreateMap<ResaleEntity, ResaleModelView>().ReverseMap();
        CreateMap<PhoneEntity, PhoneModelView>().ReverseMap();
        CreateMap<AddressEntity, AddressModelView>().ReverseMap();
        CreateMap<OrderEntity, OrderViewModel>().ReverseMap();
        CreateMap<OrderItemEntity, OrderItemViewModel>().ReverseMap();
        CreateMap<BaseEntity, BaseViewModel>().ReverseMap();
    } 
}