

using AutoMapper;
using DomainLayer.Models.BasketModule;
using DomainLayer.Models.Identity_Module;
using DomainLayer.Models.Order_Module;
using DomainLayer.Models.Product;
using Shared.DTOS;
using Shared.DTOS.BasketDTo;
using Shared.DTOS.IdentityDtos;
using Shared.DTOS.Order;

namespace Service
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            #region Product
            CreateMap<Product, ProductDto>()
                   .ForMember(dist => dist.BrandName, options => options.MapFrom(src => src.ProductBrand.Name))
                   .ForMember(dist => dist.TypeName, options => options.MapFrom(src => src.ProductType.Name))
                   .ForMember(dist => dist.PictureUrl, options => options.MapFrom<PictureUrlResolver>());

            CreateMap<ProductType, TypeDto>();
            CreateMap<ProductBrand, BrandDto>();
            #endregion
            #region Basket
            CreateMap<CustumerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            #endregion
            #region Identity
            CreateMap<Address, AddressDto>().ReverseMap();
            #endregion
            #region Order
            //CreateMap<OrderDto, Order>();
            CreateMap<ShippingAddressDto, ShippingAddress>().ReverseMap();
            CreateMap<DeliveryMethod, DeliveryMethodDto>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.GetTotal()))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus.ToString()))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address)); 
            

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.Product.PictureUrl));
            #endregion
        }
    }
}
