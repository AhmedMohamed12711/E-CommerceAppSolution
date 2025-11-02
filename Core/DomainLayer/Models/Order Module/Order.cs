using DomainLayer.Models.Product;

namespace DomainLayer.Models.Order_Module
{
    public class Order:BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order(string _UserEmail
            ,ShippingAddress _Address,DeliveryMethod _DeliveryMethod
            ,ICollection<OrderItem> _Items
            ,decimal _SubTotal)
        {
            UserEmail = _UserEmail;
            Address = _Address;
            DeliveryMethod = _DeliveryMethod;
            Items = _Items;
            SubTotal = _SubTotal;
        }
        public string UserEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.Now; 
        public OrderStatus OrderStatus { get; set; }
        public ShippingAddress Address { get; set; }= default!;
        public DeliveryMethod DeliveryMethod { get; set; }= default!;
        public int DeliveryMethodId {  get; set; }//FK
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal SubTotal {  get; set; }
        public decimal GetTotal()=>SubTotal + DeliveryMethod.Price;
        

    }
}
