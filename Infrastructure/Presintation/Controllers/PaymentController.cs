using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOS.BasketDTo;


namespace Presintation.Controllers
{
    public class PaymentController(IServiceManager _serviceManager) : APIBaseController
    {
        [Authorize]
        [HttpPost("{BasketId}")]
        public async Task<ActionResult<BasketDto>>CreateOrUpdatePaymentIntentAsync(string BasketId)
        {
            var Basket = _serviceManager.paymentService.CreateOrUpdatePaymentAsync(BasketId);
            return Ok(Basket);
        }
    }
}
