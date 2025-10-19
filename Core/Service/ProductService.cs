
using AutoMapper;
using DomainLayer.Constracts;
using DomainLayer.Models;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.DTOS;
using Shared.Enums;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper): IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
           var Brands=await _unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(ProductQueryParameter queryParams)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(queryParams);
            var Products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
        }



        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
        }

        public async Task<ProductDto?> GetProductByIdAsync(int Id)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(Id);
            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(spec);
            return _mapper.Map<Product, ProductDto>(Product);
         }
    }
}
