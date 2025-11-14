using DomainLayer.Constracts;
using StackExchange.Redis;

namespace Presistance.Repository
{
    public class CashRepository(IConnectionMultiplexer connection) : ICashRepository
    {
        private readonly IDatabase _database=connection.GetDatabase();
        public async Task<string?> GetAsync(string CashKey)
        {
            var CashValue=await _database.StringGetAsync(CashKey);
            return CashValue.IsNullOrEmpty ? null : CashValue.ToString();
        }

        public async Task SetAsync(string CashKey, string Cashvalue, TimeSpan TimeToLive)
        {
            await _database.StringSetAsync(CashKey, Cashvalue, TimeToLive);
        }
    }
}
