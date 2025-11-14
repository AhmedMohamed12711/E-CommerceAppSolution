using DomainLayer.Models.Order_Module;

namespace Service.Specifications
{
    public class OrderSpecification:BaseSpecification<Order,Guid>
    {
        //Get All
        public OrderSpecification(string Email):base(o=>o.buyerEmail==Email)
        {
            AddInclude(o=>o.DeliveryMethod);   
            AddInclude(o=>o.items);
            AddOrderByDesinding(o => o.orderDate);
        }
        //Get By Id
        public OrderSpecification(Guid id):base(o=>o.Id==id) 
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.items);
        }
    }
}
