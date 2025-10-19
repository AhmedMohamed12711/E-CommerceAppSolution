
using Shared;
using Shared.DTOS;
using Shared.Enums;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        //GetAllProduct
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(ProductQueryParameter queryParams);
        //GetProductById
        Task<ProductDto?> GetProductByIdAsync(int Id);
        //GetBrand
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        //GetTypes
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();

    }
}
