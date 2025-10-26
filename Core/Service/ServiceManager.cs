

using AutoMapper;
using DomainLayer.Constracts;
using ServiceAbstraction;

namespace Service
{
    public class ServiceManager(IMapper _mapper,IUnitOfWork _unitOfWork,IBaskerRepository _baskerRepository) : IServiceManager
    {
        private readonly Lazy<IProductService> _productService=new Lazy<IProductService>(()=>new ProductService(_unitOfWork,_mapper));
        private readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>(()=>new BasketService(_baskerRepository,_mapper));
        public IProductService ProductService =>_productService.Value;

        public IBasketService BasketService => _basketService.Value;
    }
}
