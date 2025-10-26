using DomainLayer.Models.BasketModule;

namespace DomainLayer.Constracts
{
    public interface IBaskerRepository
    {
        Task<CustumerBasket?> GetBasketAsync(string key);
        Task<CustumerBasket?> CreateOrUpadateBasketAsync(CustumerBasket basket, TimeSpan? TimeToLive = null);
        Task<bool>DeleteBasketAsync(string key);
    }
}
