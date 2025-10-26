
using DomainLayer.Constracts;
using DomainLayer.Models.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Text.Json;

namespace Presistance.Data.DataSeeding
{
    public class DataSeeding (StoreDbContext _dbContext): IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
            if (PendingMigrations.Any())
            {
                _dbContext.Database.Migrate();
            }
            #region Seeding Data
            #region ProductBrand
            if (!_dbContext.ProductBrands.Any())
            {
                var BrandsData = File.OpenRead(@"..\Infrastructure\Presistance\Data\DataSeeding\brands.json");
                //بحولها من نص لنوع list from productBrand
                var Brands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(BrandsData);
                if (Brands is not null&& Brands.Any()) 
                {
                   await _dbContext.ProductBrands.AddRangeAsync(Brands); //Local
                }
            }
            #endregion
            #region ProductType
            if (!_dbContext.ProductTypes.Any())
            {
                var TypesData= File.OpenRead(@"..\Infrastructure\Presistance\Data\DataSeeding\types.json");
                var Types=await JsonSerializer.DeserializeAsync<List<ProductType>>(TypesData);
                if(Types is not null&& Types.Any())
                {
                  await  _dbContext.ProductTypes.AddRangeAsync(Types); //Local
                }
            }
            #endregion
            #region Product
            if (!_dbContext.Products.Any())
            {
                var ProductsData= File.OpenRead(@"..\Infrastructure\Presistance\Data\DataSeeding\products.json");
                var Products =await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);
                if(Products.Any()&& Products is not null)
                {
                   await _dbContext.Products.AddRangeAsync(Products);
                }
            }
            #endregion
           await _dbContext.SaveChangesAsync();
            #endregion
        }
    }
}
