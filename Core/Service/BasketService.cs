using AutoMapper;
using DomainLayer.Constracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using ServiceAbstraction;
using Shared.DTOS.BasketDTo;

namespace Service
{
    public class BasketService(IBaskerRepository _baskerRepository,IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto?> CreateOrUpdateAsync(BasketDto basket)
        {
            var CustomerBasket = _mapper.Map<BasketDto, CustumerBasket>(basket);
            var CreatedOrUpdated=await _baskerRepository.CreateOrUpadateBasketAsync(CustomerBasket);
            if (CreatedOrUpdated is not null) 
            {
                return await GetBasketAsync(basket.Id);   
            }
            else
            {
                throw new Exception("Can not Update Or Create Basket Now ,Try Again Later");
            }
        }

        public async Task<bool> DeleteBasketAsync(string key)=>await _baskerRepository.DeleteBasketAsync(key);
       

        public async Task<BasketDto?> GetBasketAsync(string Key)
        {
            var basket=await _baskerRepository.GetBasketAsync(Key);
            if (basket == null) throw new BasketNotFoundException(Key);
            else
            {
                return _mapper.Map<CustumerBasket,BasketDto>(basket);
            }
                    
        }
    }
}
