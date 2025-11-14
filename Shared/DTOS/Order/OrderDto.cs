using Shared.DTOS.IdentityDtos;

namespace Shared.DTOS.Order
{
    public class OrderDto
    {
        public string BasketId { get; init; } = string.Empty;
        public int DeliveryMethodId { get; init; }
        public ShippingAddressDto ShipToAddress { get; init; }
    }
}
