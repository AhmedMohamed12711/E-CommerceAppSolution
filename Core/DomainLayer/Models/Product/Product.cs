namespace DomainLayer.Models.Product
{
    public class Product:BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = string.Empty;

        // ✅ نحافظ على الـ FKs والعلاقات لكن نغير أسماء الخصائص لتطابق الـ DTO
        #region Product Brand
        public int BrandId { get; set; }
        public ProductBrand ProductBrand { get; set; } = null!;

        #endregion

        #region Product Type
        public int TypeId { get; set; }
        public ProductType ProductType { get; set; } = null!;

        #endregion

    }
}
