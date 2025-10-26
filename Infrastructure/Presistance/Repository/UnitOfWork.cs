using DomainLayer.Constracts;
using DomainLayer.Models.Product;
using Presistance.Data;


namespace Presistance.Repository
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _Repositorise = [];
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            //TypeName of Entity
            var TypeName=typeof(TEntity).Name;//Ex=>product
            //Dic<String,object> string[Key]=>[Name of type] == object[Value]=>object from generic Repository
            //if (_Repositorise.ContainsKey(TypeName))
            //{

                //return (IGenericRepository<TEntity, Tkey>) _Repositorise[TypeName];
            //}
            if(_Repositorise.TryGetValue(TypeName,out object? value))
                return (IGenericRepository<TEntity, Tkey>)value;
            
            else
            {
                var Repo =new  GenericRepository<TEntity, Tkey>(_dbContext);
                _Repositorise["TypeName"] = Repo;
                return Repo;
            }
        }

        public async Task<int> SaveChangesAsync()=> await _dbContext.SaveChangesAsync();
      
    }
}
