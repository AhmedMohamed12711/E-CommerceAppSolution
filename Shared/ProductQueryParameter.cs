using Shared.Enums;

namespace Shared
{
    public class ProductQueryParameter
    {
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public ProductSortingOptions SortingOptions { get; set; }
        public string? SearchValue { get; set; }
        #region Pagination
        private const int DefaultPageSize = 5;
        private const int MaxPageSize = 10;
        public int pageIndex { get; set; } = 1;
        private int pageSize=DefaultPageSize;
        public int PageSize 
        {
            get {  return pageSize; }
            set { pageSize = value>MaxPageSize?MaxPageSize:value; }
        }
        #endregion

    }
}
