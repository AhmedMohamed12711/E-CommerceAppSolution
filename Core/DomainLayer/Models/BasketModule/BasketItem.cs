namespace DomainLayer.Models.BasketModule
{
    //In Memory Database
    public class BasketItem
    {
        public string Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string?PictureUrl{ get; set; }
        public decimal Price {  get; set; }
        public int Quantity {  get; set; }
    }
}
