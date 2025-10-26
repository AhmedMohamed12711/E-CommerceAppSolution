using Shared.DTOS.BasketDTo;

namespace ServiceAbstraction
{
    public interface IBasketService
    {
        Task<BasketDto?> GetBasketAsync(string Key);
        Task<BasketDto?> CreateOrUpdateAsync(BasketDto basket);
        Task<bool> DeleteBasketAsync(string key);
    }
}
