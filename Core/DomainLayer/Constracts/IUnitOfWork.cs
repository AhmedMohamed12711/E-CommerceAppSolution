

using DomainLayer.Models;

namespace DomainLayer.Constracts
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity,Tkey>GetRepository<TEntity, Tkey>() where TEntity:BaseEntity<Tkey>;
         Task<int> SaveChangesAsync();
    }
}
