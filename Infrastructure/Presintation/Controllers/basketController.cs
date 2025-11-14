using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOS.BasketDTo;
namespace Presintation.Controllers
{
    
    public class basketController(IServiceManager _serviceManager):APIBaseController
    {
        //GetBasket
        [HttpGet]
        public async Task<ActionResult<BasketDto>>GetBasket(string key)
        {
            var Basket=await _serviceManager.BasketService.GetBasketAsync(key);
            return Ok(Basket);
        }
        //CreateOrUpdateBasket
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket= await _serviceManager.BasketService.CreateOrUpdateAsync(basket);
            return Ok(Basket);
        }
        [HttpDelete("{Key}")]
        public async Task<ActionResult<bool>> DeleteBasket(string Key) 
        {
            var Result=await _serviceManager.BasketService.DeleteBasketAsync(Key);
            return Ok(Result) ;
        }
    }
}
