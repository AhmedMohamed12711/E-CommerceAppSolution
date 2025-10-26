namespace Shared.DTOS.BasketDTo
{
    public class BasketItemDto
    {
        public string Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
