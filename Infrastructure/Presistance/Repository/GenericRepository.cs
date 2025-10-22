using DomainLayer.Constracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Presistance.Data;
using Service.Specifications;

namespace Presistance.Repository
{
    public class GenericRepository<TEntity, Tkey>(StoreDbContext _dbContext) : IGenericRepository<TEntity, Tkey> where TEntity:BaseEntity<Tkey>
    {

        public async Task AddAsync(TEntity entity)=>await _dbContext.Set<TEntity>().AddAsync(entity);



        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbContext.Set<TEntity>().ToListAsync ();

     

        public void Remove(TEntity entity)=> _dbContext.Set<TEntity>().Remove(entity);
        

        public async Task<TEntity?> GetByIdAsync(Tkey id) => await _dbContext.Set<TEntity>().FindAsync(id);
        public void Update(TEntity entity)=>_dbContext.Set<TEntity>().Update(entity);
        #region With Specefication

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, Tkey> specification)
        {
            return await SpecificationEvaluated.CreateQuery(_dbContext.Set<TEntity>(), specification).ToListAsync();
        }


        public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity, Tkey> specification)
        {
            return await SpecificationEvaluated.CreateQuery(_dbContext.Set<TEntity>(),specification).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecification<TEntity, Tkey> specification)
        {
            return await SpecificationEvaluated.CreateQuery(_dbContext.Set<TEntity>(),specification).CountAsync();
        }
        #endregion

    }
}
