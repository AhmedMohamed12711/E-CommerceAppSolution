using DomainLayer.Constracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public abstract class BaseSpecification<TEntity, Tkey> : ISpecification<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public BaseSpecification(Expression<Func<TEntity, bool>> CriteriaExpression)
        {
            Criteria= CriteriaExpression;
        }
        public Expression<Func<TEntity, bool>>? Criteria {  get;private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; } = [];

        public Expression<Func<TEntity, object>> OrderBy {  get; private set; }
        protected void AddOrderBy(Expression<Func<TEntity, object>> OrderByAsc)=>OrderBy=OrderByAsc;
        
        public Expression<Func<TEntity, object>> OrderByDescding { get; private set; }
        protected void AddOrderByDesinding(Expression<Func<TEntity, object>> OrderByDesc) => OrderByDescding = OrderByDesc;


        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) 
        {
            IncludeExpression.Add(includeExpression);
        }
    }
}
