using DomainLayer.Models;
using DomainLayer.Models.Product;
using Shared;
using System.Linq.Expressions;

namespace Service.Specifications
{
    public class ProductCountSpecification(ProductQueryParameter queryParams) : BaseSpecification<Product, int>(P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId)
            && (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId)
            && (string.IsNullOrWhiteSpace(queryParams.search) || P.Name.Contains(queryParams.search.ToLower())))
    {

    }
}
