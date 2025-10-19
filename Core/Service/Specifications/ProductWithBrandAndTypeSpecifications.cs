using DomainLayer.Models;
using Shared;
using Shared.Enums;
using System.Linq.Expressions;

namespace Service.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecification<Product, int>
    {
        //GetAllProducts With Types And Brands
        public ProductWithBrandAndTypeSpecifications(ProductQueryParameter queryParams) : 
            base(P=>(!queryParams.BrandId.HasValue || P.BrandId==queryParams.BrandId)
            && (!queryParams.TypeId.HasValue || P.TypeId==queryParams.TypeId)
            &&(string.IsNullOrWhiteSpace(queryParams.SearchValue)||P.Name.Contains(queryParams.SearchValue.ToLower())))   
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            #region Sorting
            switch (queryParams.SortingOptions)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;
                    case ProductSortingOptions.NameDesc:
                    AddOrderByDesinding(P => P.Name);
                     break;
                    case ProductSortingOptions.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                    case ProductSortingOptions.PriceDesc:
                    AddOrderByDesinding(P => P.Price);
                        break;
                    default:
                    break;
            }
            #endregion
        }
        //Get Product By ID
        public ProductWithBrandAndTypeSpecifications(int Id) :base(P=>P.Id==Id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            
        }

    }
}
