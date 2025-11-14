
using DomainLayer.Constracts;
using DomainLayer.Models.Identity_Module;
using E_CommerceApp.CustomMiddleware;
using E_CommerceApp.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Presistance.Data;
using Presistance.Data.DataSeeding;
using Presistance.Repository;
using Service;
using ServiceAbstraction;
using Shared.ErrorModel;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;
using System.Text.Json;
using Microsoft.OpenApi.Models;

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
            #region ConnectionString
            builder.Services.AddDbContext<StoreDbContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                });
            builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            }); 
            #endregion
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(X => X.AddProfile(new MappingProfile()));
            #region DelegetFactory
            builder.Services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegete>();//------

            builder.Services.AddScoped<IProductService, ProductService>();//--------
            builder.Services.AddScoped<Func<IProductService>>(provider =>
                () => provider.GetRequiredService<IProductService>());

            builder.Services.AddScoped<IOrderService, OrderService>();//--------
            builder.Services.AddScoped<Func<IOrderService>>(provider =>
                () => provider.GetRequiredService<IOrderService>());

            builder.Services.AddScoped<IBasketService, BasketService>();//--------
            builder.Services.AddScoped<Func<IBasketService>>(provider =>
                () => provider.GetRequiredService<IBasketService>());

            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();//--------
            builder.Services.AddScoped<Func<IAuthenticationService>>(provider =>
                () => provider.GetRequiredService<IAuthenticationService>());

            builder.Services.AddScoped<IPaymentService, PaymentService>();//--------
            builder.Services.AddScoped<Func<IPaymentService>>(provider =>
                () => provider.GetRequiredService<IPaymentService>());
            #endregion

            builder.Services.AddScoped<PictureUrlResolver>();
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = APIesponseFactory.GenerateApiValdiationErrorResponse;


            });
            builder.Services.AddScoped<IBaskerRepository, BaskerRepository>();
            builder.Services.AddScoped<IBasketService, BasketService>();
            #region Connection With Angular
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();

                });
            });
            //call before app authorizatio
            #endregion
            builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RediusConnection"));
            });

            builder.Services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();

            builder.Services.AddScoped<ICashService, CashService>();
            builder.Services.AddScoped<ICashRepository, CashRepository>();
            #region JWTAuthentication
            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                }).AddJwtBearer(options =>
                {
                    var jwtSettings = builder.Configuration.GetSection("JWTOptions");
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(jwtSettings["Secretkey"]!))
                    };
                });
            builder.Services.AddScoped<IOrderService, OrderService>();
            
            
            #endregion
            #endregion

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In=ParameterLocation.Header,
                    Name="Authorization",
                    Type=SecuritySchemeType.ApiKey,
                    Scheme="Bearer",
                    Description="Enter 'Bearer' Followed By Space And Your Token"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference=new OpenApiReference()
                            {
                                Id="Bearer",
                                Type=ReferenceType.SecurityScheme,
                            }
                        },
                        new string[] { }
                    }
                });
            });
             #region Build
            var app = builder.Build();
            #endregion
            //???? ???? ?? UpdateDatabase
            #region Service
            var Scope = app.Services.CreateScope();
            var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
             await ObjectOfDataSeeding.DataSeedAsync();
            await ObjectOfDataSeeding.IdentityDataSeed();
           
            #endregion
            #region MiddleWare
            app.UseMiddleware<CustomExceptionHandelerMiddleware>(); 
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.ConfigObject = new ConfigObject()
                    {
                        DisplayRequestDuration=true,
                        
                    };
                    options.DocumentTitle = "My E-Commerce API";
                    options.JsonSerializerOptions = new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };
                    options.DocExpansion(DocExpansion.None);
                    options.EnableFilter();
                    options.EnablePersistAuthorization();
                });
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseCors("AllowAll");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
