using DomainLayer.Models.Order_Module;

namespace Service.Specifications
{
    public class OrderWithPaymentIntentIdSpecifications:BaseSpecification<Order,Guid>
    {
        public OrderWithPaymentIntentIdSpecifications(string PaymentIntentId)
            :base(o=>o.PaymentIntentId==PaymentIntentId)
        {
            
        }
    }
}
