
using AutoMapper;
using DomainLayer.Models.Product;
using Microsoft.Extensions.Configuration;
using Shared.DTOS;

namespace Service
{
    public class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductDto, string>
    {
        //https://localhost:7112/{src.PictureUrl}
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty;
            }
            else
            {
                var URl = $"{_configuration.GetSection("URLS")["BaseUrl"]}{source.PictureUrl}";
                return URl;
            }
        }
    }
}
