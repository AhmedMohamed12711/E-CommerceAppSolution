namespace Shared.DTOS.Order
{
    public class OrderDto
    {
        public string BasketId { get; set; } = default!;
        public int DeliveryMethodId {  get; set; }
       public ShippingAddressDto Address {  get; set; }=default!;
    }
}
