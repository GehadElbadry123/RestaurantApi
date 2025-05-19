
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Restaurant_API.Data;
using Restaurant_API.MapperConfig;
using Restaurant_API.Models;
using Restaurant_API.Repository;

namespace Restaurant_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            builder.Services.AddDbContext<ApplicationDbContext>(options =>

                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))

            );


            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddAutoMapper(typeof(mappconfig));
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IOrderRepository,OrderRepository>();
            builder.Services.AddScoped<IOrderItemRepository,OrderItemRepository>();


            //jwt 

            builder.Services.AddAuthentication("myschema")
                 .AddJwtBearer("myschema", options =>
                 {
                     var key = "welcome to my sercert key mansoura branch";
                     var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = false,
                         ValidateAudience = false,
                         IssuerSigningKey = secretKey,
                         ValidateIssuerSigningKey = true
                     };
                 });

            builder.Services.AddAuthorization();
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();



            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:4200") // Angular URL
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            





            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowFrontend");


            app.MapControllers();

            app.Run();
        }
    }
}
