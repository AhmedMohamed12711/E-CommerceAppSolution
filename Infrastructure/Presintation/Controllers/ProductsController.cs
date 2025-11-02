using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Presintation.Controllers;
using ServiceAbstraction;
using Shared;
using Shared.DTOS;
using Shared.Enums;

namespace Presintation.Controller
{
   
    public class ProductsController(IServiceManager _serviceManager):APIBaseController
    {
        #region GetAllProduct
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParameter queryParams)
        {
            var products=await _serviceManager.ProductService.GetAllProductsAsync(queryParams);
            return Ok(products);
        }
        #endregion

        #region GetProductById
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int Id)
        {
            var Product=await _serviceManager.ProductService.GetProductByIdAsync(Id);
            return Ok(Product);
        }
        #endregion

        #region GetBrands
        [HttpGet("Brands")]

        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var Brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }
        #endregion
        #region GetTypes
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            var Types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }
        #endregion
    }
}
