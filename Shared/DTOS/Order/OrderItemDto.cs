namespace Shared.DTOS.Order
{
    public record OrderItemDto
    {
        public int ProductId { get; init; }
        public string ProductName { get; init; } = string.Empty;
        public string PictureUrl { get; init; } = string.Empty;

        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
