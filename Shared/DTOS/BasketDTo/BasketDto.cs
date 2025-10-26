namespace Shared.DTOS.BasketDTo
{
    public class BasketDto
    {
        public string Id { get; set; }
        public ICollection<BasketItemDto> Items { get; set; }
    }
}
