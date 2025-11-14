namespace DomainLayer.Models.BasketModule
{
    public class CustumerBasket
    {
        public string Id { get; set; }//GUID : CreatedFrom Client [FrontEnd]
        public ICollection<BasketItem> Items { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public decimal? ShippingPrice { get; set; }//DeliveryMethod.Price
        public int? DeliveryMethodId { get; set; }
    }
}
