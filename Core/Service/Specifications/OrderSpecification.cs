using DomainLayer.Models.Order_Module;

namespace Service.Specifications
{
    public class OrderSpecification:BaseSpecification<Order,Guid>
    {
        //Get All
        public OrderSpecification(string Email):base(o=>o.UserEmail==Email)
        {
            AddInclude(o=>o.DeliveryMethod);   
            AddInclude(o=>o.Items);
            AddOrderByDesinding(o => o.OrderDate);
        }
        //Get By Id
        public OrderSpecification(Guid id):base(o=>o.Id==id) 
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
        }
    }
}
