
using DomainLayer.Constracts;
using E_CommerceApp.CustomMiddleware;
using E_CommerceApp.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presistance.Data;
using Presistance.Data.DataSeeding;
using Presistance.Repository;
using Service;
using ServiceAbstraction;
using Shared.ErrorModel;
using StackExchange.Redis;

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
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = APIesponseFactory.GenerateApiValdiationErrorResponse;


            });
            builder.Services.AddScoped<IBaskerRepository, BaskerRepository>();
            builder.Services.AddScoped<IBasketService, BasketService>();

            builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RediusConnection"));
            });

            #endregion

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
             #region Build
            var app = builder.Build();
            #endregion
            //???? ???? ?? UpdateDatabase
            #region Service
            var Scope = app.Services.CreateScope();
            var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
           await ObjectOfDataSeeding.DataSeedAsync();
            #endregion
            #region MiddleWare
            app.UseMiddleware<CustomExceptionHandelerMiddleware>(); 
            #endregion

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
