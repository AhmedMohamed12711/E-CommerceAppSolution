
using DomainLayer.Constracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
namespace Service.Specifications
{
    public static class SpecificationEvaluated
    {
        //CreateQuery
        // _dbContext.Products.Where(P => P.Id == id).Include(P => P.ProductType).Include(P => P.ProductBrand);
        public static IQueryable<TEntity> CreateQuery<TEntity,Tkey>(IQueryable<TEntity> InputQuery,ISpecification<TEntity,Tkey> specification) where TEntity : BaseEntity<Tkey>
        {
                var Query = InputQuery;
            if(specification.Criteria is not null)
            {
                Query = Query.Where(specification.Criteria);
            }
            if(specification.OrderBy is not null)
            {
                Query = Query.OrderBy(specification.OrderBy);
            }
            if(specification.OrderByDescding is not null)
            {
                Query = Query.OrderByDescending(specification.OrderByDescding);
            }
            if(specification.IncludeExpression is not null && specification.IncludeExpression.Count > 0)
            {
                Query = specification.IncludeExpression.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));
            }
            if (specification.IsPaginated)
            {
                Query=Query.Skip(specification.Skip).Take(specification.Take);
            }

            return Query;
        }
    }
}
