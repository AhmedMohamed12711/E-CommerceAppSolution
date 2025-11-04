

using AutoMapper;
using DomainLayer.Constracts;
using DomainLayer.Models.Identity_Module;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;

namespace Service
{
    public class ServiceManager(IMapper _mapper,IUnitOfWork _unitOfWork,IBaskerRepository _baskerRepository,UserManager<ApplicationUser> _userManager,IConfiguration configuration) : IServiceManager
    {
        private readonly Lazy<IProductService> _productService=new Lazy<IProductService>(()=>new ProductService(_unitOfWork,_mapper));
        private readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>(()=>new BasketService(_baskerRepository,_mapper));
        private readonly Lazy<IAuthenticationService> _authenticationService = new Lazy<IAuthenticationService>(()=>new AuthenticationService(_userManager,configuration,_mapper));
        private readonly Lazy<IOrderService> _orderService = new Lazy<IOrderService>(()=>new OrderService(_unitOfWork,_baskerRepository,_mapper));
        public IProductService ProductService =>_productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public IOrderService OrderService => _orderService.Value;
    }
}
