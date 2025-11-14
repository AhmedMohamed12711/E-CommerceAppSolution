using DomainLayer.Models.Product;

namespace DomainLayer.Models.Order_Module
{
    public class DeliveryMethod:BaseEntity<int>
    {
        public int Id { get; init; }
        public string ShortName { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal Cost { get; init; }
        public string DeliveryTime { get; init; } = string.Empty;

    }
}
