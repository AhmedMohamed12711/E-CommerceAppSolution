
namespace Shared.DTOS
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public string productBrand {  get; set; } = null!;
        public string productType { get; set;} = null!;
    }
}
