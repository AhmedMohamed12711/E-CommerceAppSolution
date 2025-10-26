namespace DomainLayer.Models.BasketModule
{
    public class CustumerBasket
    {
        public string Id {  get; set; }
        public ICollection<BasketItem> Items { get; set; } = [];
    }
}
