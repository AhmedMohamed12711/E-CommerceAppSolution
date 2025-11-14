using DomainLayer.Models.Product;

namespace DomainLayer.Models.Order_Module
{
    public class OrderItem:BaseEntity<int>
    {
        public ProductItemOrder Product { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
