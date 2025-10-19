

using AutoMapper;
using DomainLayer.Constracts;
using ServiceAbstraction;

namespace Service
{
    public class ServiceManager(IMapper _mapper,IUnitOfWork _unitOfWork) : IServiceManager
    {
        private readonly Lazy<IProductService> _productService=new Lazy<IProductService>(()=>new ProductService(_unitOfWork,_mapper));
        public IProductService ProductService =>_productService.Value;
    }
}
