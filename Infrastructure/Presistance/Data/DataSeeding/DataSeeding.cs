
using DomainLayer.Constracts;
using DomainLayer.Models.Identity_Module;
using DomainLayer.Models.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Text.Json;

namespace Presistance.Data.DataSeeding
{
    public class DataSeeding (StoreDbContext _dbContext,UserManager<ApplicationUser> _userManager,RoleManager<IdentityRole> _roleManager): IDataSeeding
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

        //SeedIdentity
        public async Task IdentityDataSeed()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    //Add Role
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    //Add User
                    var User01 = new ApplicationUser()
                    {
                        Email = "Mohammed@gmail.com",
                        DisplayName = "Mohammed Ali",
                        PhoneNumber = "3511656132",
                        UserName = "MohammedAli",
                        EmailConfirmed = true,
                    };
                    var User02 = new ApplicationUser()
                    {
                        Email = "Salma@gmail.com",
                        DisplayName = "Salma Ali",
                        PhoneNumber = "3511656132",
                        UserName = "SalmaAli",
                        EmailConfirmed = true,
                    };

                    await _userManager.CreateAsync(User01, "P@ssw0rd");
                    await _userManager.CreateAsync(User02, "P@ssw0rd");
                    //Add Role To User
                    await _userManager.AddToRoleAsync(User01, "Admin");
                    await _userManager.AddToRoleAsync(User02, "SuperAdmin");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
