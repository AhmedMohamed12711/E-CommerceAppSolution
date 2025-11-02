using DomainLayer.Models.Order_Module;
using DomainLayer.Models.Product;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace Presistance.Data
{
    public class StoreDbContext(DbContextOptions<StoreDbContext> options):DbContext(options)
    {
        #region Dbset
        public DbSet<Product> Products {  get; set; }
        public DbSet<ProductBrand> ProductBrands {  get; set; }
        public DbSet<ProductType> ProductTypes {  get; set; }
        
        public DbSet<Order> Orders { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
