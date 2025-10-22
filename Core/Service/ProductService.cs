
using AutoMapper;
using DomainLayer.Constracts;
using DomainLayer.Exceptions;
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

        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParameter queryParams)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(queryParams);
            var Repo =  _unitOfWork.GetRepository<Product, int>();
            var AllProducts = await Repo.GetAllAsync(spec);
            var Data= _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(AllProducts);

            var ProductCount=AllProducts.Count();
            var CountSpec = new ProductCountSpecification(queryParams);
            var TotalCount=await Repo.CountAsync(CountSpec);
            return new PaginatedResult<ProductDto>(ProductCount,queryParams.pageIndex,TotalCount,Data);
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
            if(Product == null)
            {
                throw new ProductNotFoundException(Id);
            }
            return _mapper.Map<Product, ProductDto>(Product);
         }
    }
}
