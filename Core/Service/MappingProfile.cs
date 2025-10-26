

using AutoMapper;
using DomainLayer.Models.BasketModule;
using DomainLayer.Models.Product;
using Shared.DTOS;
using Shared.DTOS.BasketDTo;

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
        }
    }
}
