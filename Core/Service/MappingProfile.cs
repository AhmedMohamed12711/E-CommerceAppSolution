

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
                   .ForMember(dist => dist.productBrand, options => options.MapFrom(src => src.ProductBrand.Name))
                   .ForMember(dist => dist.productType, options => options.MapFrom(src => src.ProductType.Name))
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
            CreateMap<ShippingAddress, ShippingAddressDto>().ReverseMap();
            CreateMap<DeliveryMethod, DeliveryMethodDto>();
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, options => options.MapFrom(s => s.Product.ProductId))
                .ForMember(dest => dest.ProductName, options => options.MapFrom(s => s.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom(s => s.Product.PictureUrl));


            CreateMap<Order, OrderToReturnDto>();
                
            

           
            #endregion
        }
    }
}
