using DomainLayer.Models;
using System.Linq.Expressions;

namespace DomainLayer.Constracts
{
    public interface ISpecification<TEntity,Tkey> where TEntity:BaseEntity<Tkey>
    {
        public Expression<Func<TEntity, bool>>? Criteria {  get; }
        List<Expression<Func<TEntity, object>>> IncludeExpression {  get; }
        #region Sorting
        Expression<Func<TEntity, object>> OrderBy { get; }
        Expression<Func<TEntity, object>> OrderByDescding { get; }
        #endregion
        #region Pagination
        public int Take { get; }
        public int Skip {  get; }
        public bool IsPaginated  { get; set; }
        #endregion

    }
}
