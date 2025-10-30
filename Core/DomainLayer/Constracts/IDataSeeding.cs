
namespace DomainLayer.Constracts
{
    public interface IDataSeeding
    {
        public  Task DataSeedAsync();
        public Task IdentityDataSeed();
    }
}
