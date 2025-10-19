using Shared.Enums;

namespace Shared
{
    public class ProductQueryParameter
    {
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public ProductSortingOptions SortingOptions { get; set; }
        public string ?SearchValue {  get; set; }

    }
}
