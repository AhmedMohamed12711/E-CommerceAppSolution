using DomainLayer.Models.Product;

namespace DomainLayer.Constracts
{
    public interface IGenericRepository<TEntity,Tkey> where TEntity:BaseEntity<Tkey>
    {
        //GetAll
        Task<IEnumerable<TEntity>>  GetAllAsync();
        //GetById
        Task<TEntity?> GetByIdAsync(Tkey id);
        //Add
        Task AddAsync(TEntity entity);
        //Update
        void Update(TEntity entity);
        //Delete
        void Remove(TEntity entity);
        #region With Specification
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity,Tkey> specification);
        Task<TEntity?> GetByIdAsync(ISpecification<TEntity, Tkey> specification);
        Task<int> CountAsync(ISpecification<TEntity, Tkey> specification);
        #endregion
    }
}
