using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOS.Order;
using System.Security.Claims;

namespace Presintation.Controllers
{
    public class ordersController(IServiceManager _serviceManager):APIBaseController
    {
        #region CreateOrder
        [Authorize]
        [HttpPost("CreateOrder")]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            if (Email == null)
            {
                throw new UnauthorizedAccessException("User Email not Found in token");
            }
            var Result = await _serviceManager.OrderService.CreateOrder(orderDto, Email);
            return Ok(Result);
        }
        #endregion
        #region GetAllDeliveryMethod
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var DeliveryMethod=await _serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(DeliveryMethod);
        }
        #endregion
        #region GetAllOrder
        [Authorize]
        [HttpGet("AllOrders")]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrdersAsync()
        {
            var Email=User.FindFirstValue(ClaimTypes.Email);
            var Order = await _serviceManager.OrderService.GetAllOrdersAsync(Email!);
            return Ok(Order);
        }

        #endregion
        #region GetAllOrderById
        [Authorize]
        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid Id)
        {
            var Order = await _serviceManager.OrderService.GetOrderByIdAsync(Id);
            return Ok(Order);
        }
        #endregion
    }
}
