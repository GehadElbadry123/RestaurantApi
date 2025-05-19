using Microsoft.EntityFrameworkCore;
using Restaurant_API.Models;

namespace Restaurant_API.Data
{
    public class ApplicationDbContext: DbContext
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Appetizers" },
                new Category { Id = 2, Name = "Main Course" },
                new Category { Id = 3, Name = "Desserts" },
                new Category { Id = 4, Name = "Drinks" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Cheese Balls",
                    Description = "Delicious fried cheese balls.",
                    Price = 45.00m,
                    ImageUrl = "images/cheese-balls.jpg",
                    CategoryId = 1,
                    IsAvailable = true
                },
                new Product
                {
                    Id = 2,
                    Name = "Grilled Chicken",
                    Description = "Juicy grilled chicken breast with veggies.",
                    Price = 120.00m,
                    ImageUrl = "images/grilled-chicken.jpg",
                    CategoryId = 2,
                    IsAvailable = true
                }
            );

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Admin",
                    Email = "admin@restaurant.com",
                    Password = "admin123", 
                    Role = "Admin"
                }
            );

        }

    }
}
