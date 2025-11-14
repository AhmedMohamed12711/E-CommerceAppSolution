
using Shared.DTOS.IdentityDtos;

namespace Shared.DTOS.Order
{
    public record OrderToReturnDto
    {
        public Guid Id { get; init; }
        public string buyerEmail { get; init; } = string.Empty;
        public ShippingAddressDto shipToAddress { get; init; }
        public ICollection<OrderItemDto> items { get; init; } = new List<OrderItemDto>();
        public string status { get; init; } = string.Empty;
        public string deliveryMethod { get; init; } = string.Empty;
        public int? DeliveryMethodId { get; init; }
        public decimal deliveryCost { get; set; }
        public decimal subtotal { get; init; } //OrderItem1.Q * OrderItem1.Price + OrderItem2.Q * OrderItem2.Price
        public DateTimeOffset orderDate { get; init; } = DateTimeOffset.UtcNow;
        public string PaymentIntentId { get; init; } = string.Empty;
        public decimal Total { get; init; }
    }
}
