namespace Shared.DTOS.BasketDTo
{
    public class BasketDto
    {
        public string Id { get; set; }
        public ICollection<BasketItemDto> Items { get; set; }
        public string? PaymentIntentId {  get; set; }
        public string? ClientSecret {  get; set; }
        public decimal? ShippingPrice { get; set; }
        public int?DeliveryMethodId { get; set; } 
    
    }
}
