using AutoMapper;
using DomainLayer.Constracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.Order_Module;
using DomainLayer.Models.Product;
using ServiceAbstraction;
using Shared.DTOS.Order;

namespace Service
{
    public class OrderService(IUnitOfWork _unitOfWork,IBaskerRepository _baskerRepository,IMapper _mapper) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrder(OrderDto orderDto, string Email)
        {
            //Map Address To Order Address
            var OrderAddress = _mapper.Map<ShippingAddressDto, ShippingAddress>(orderDto.Address);
            //Get Basket
            var Basket = await _baskerRepository.GetBasketAsync(orderDto.BasketId)
                ?? throw new BasketNotFoundException(orderDto.BasketId); ;
            //Create OrderItem List
            List<OrderItem> OrderItems = [];
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(int.Parse(item.Id))
                    ??throw new ProductNotFoundException(int.Parse(item.Id));
                var orderItem = new OrderItem()
                {
                    Product = new ProductItemOrder()
                    {
                        ProductId = Product.Id,
                        ProductName = Product.Name,
                        PictureUrl = Product.PictureUrl
                    },
                    Price = Product.Price,
                    Quantity = item.Quantity,

                };
                OrderItems.Add(orderItem);   
            }
            //Get DeliveryMethode
            var DeliveryMethod =await _unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(orderDto.DeliveryMethodId)??throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);
            //Calculate SubTotal
            var SubTotal = OrderItems.Sum(I => I.Quantity * I.Price);

            //Create Order
            var Order = new Order(Email, OrderAddress, DeliveryMethod, OrderItems, SubTotal);
            //Add order To Database
            await _unitOfWork.GetRepository<Order,Guid>().AddAsync(Order);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<Order,OrderToReturnDto>(Order);   
        }
    }
}
