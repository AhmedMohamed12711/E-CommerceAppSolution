using DomainLayer.Models.Product;

namespace DomainLayer.Models.Order_Module
{
    public class Order:BaseEntity<Guid>
    {
        public Order()
        {

        }
        public Order(string _UserEmail, ShippingAddress _Address
           , DeliveryMethod _DeliveryMethod, ICollection<OrderItem> _Items, decimal _SubTotal, string _PaymentIntentId)
        {
            buyerEmail = _UserEmail;
            shipToAddress = _Address;
            DeliveryMethod = _DeliveryMethod;
            items = _Items;
            subtotal = _SubTotal;
            PaymentIntentId = _PaymentIntentId;
        }



        public string buyerEmail { get; set; } = string.Empty;
        public DateTimeOffset orderDate { get; set; } = DateTimeOffset.Now;
        public ShippingAddress shipToAddress { get; set; } = null!;
        public DeliveryMethod DeliveryMethod { get; set; } = null!;
        public int DeliveryMethodId { get; set; }
        public OrderStatus status { get; set; }
        public ICollection<OrderItem> items { get; set; } = [];
        public decimal subtotal { get; set; }

        public string PaymentIntentId { get; set; }
        public decimal deliveryCost => DeliveryMethod.Cost;
        public decimal Total => subtotal + DeliveryMethod.Cost;


    }
}
