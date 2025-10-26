using DomainLayer.Constracts;
using DomainLayer.Models.BasketModule;
using StackExchange.Redis;
using System.Text.Json;

namespace Presistance.Repository
{
    public class BaskerRepository (IConnectionMultiplexer connection): IBaskerRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();

        public async Task<CustumerBasket?> CreateOrUpadateBasketAsync(CustumerBasket basket, TimeSpan? TimeToLive = null)
        {
            var JsonBasket=JsonSerializer.Serialize(basket);
            var IsCreatedOrupdated = await _database.StringSetAsync(basket.Id, JsonBasket, TimeToLive ?? TimeSpan.FromDays(1));
            if(IsCreatedOrupdated)
            {
                return await GetBasketAsync(basket.Id);
            }
            else
            {
                return null;
            }
        }
        public async Task<CustumerBasket?> GetBasketAsync(string key)
        {
            var Basket = await _database.StringGetAsync(key);
            if (Basket.IsNullOrEmpty) return null;
            else
            {
                return JsonSerializer.Deserialize<CustumerBasket>(Basket);
            }
        }

        public async Task<bool> DeleteBasketAsync(string key)=>await _database.KeyDeleteAsync(key);
        


    }
}
