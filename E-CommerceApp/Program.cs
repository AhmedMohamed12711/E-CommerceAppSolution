
using DomainLayer.Constracts;
using E_CommerceApp.CustomMiddleware;
using Microsoft.EntityFrameworkCore;
using Presistance.Data;
using Presistance.Data.DataSeeding;
using Presistance.Repository;
using Service;
using ServiceAbstraction;

namespace E_CommerceApp
{
    public  class Program
    {
        public  static async Task  Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            #region Configure Service
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(X => X.AddProfile(new MappingProfile()));
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddScoped<PictureUrlResolver>();
            #endregion



            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            //???? ???? ?? UpdateDatabase
            #region Service
            var Scope = app.Services.CreateScope();
            var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
           await ObjectOfDataSeeding.DataSeedAsync();
            #endregion
            app.UseMiddleware<CustomExceptionHandelerMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
