using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_API.Data;
using Restaurant_API.Models;
using System.Security.Claims;

namespace Restaurant_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<User?> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var user = await GetCurrentUser();
            if (user == null) return Unauthorized();

            // ابحث عن أوردر غير مكتمل
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.UserId == user.Id && o.Status == "Pending");

            if (order == null)
            {
                order = new Order
                {
                    UserId = user.Id,
                    OrderDate = DateTime.Now,
                    Status = "Pending",
                    TotalPrice = 0
                };
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
            }

            // هل المنتج موجود في الأوردر؟
            var existingItem = order.OrderItems.FirstOrDefault(i => i.ProductId == productId);
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
                return NotFound("Product not found");

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var newItem = new OrderItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    UnitPrice = product.Price,
                    OrderId = order.Id
                };
                order.OrderItems.Add(newItem);
            }

            // احسب السعر الكلي
            order.TotalPrice = order.OrderItems.Sum(i => i.Quantity * i.UnitPrice);
            await _context.SaveChangesAsync();

            return Ok("Product added to cart");
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var user = await GetCurrentUser();
            if (user == null) return Unauthorized();

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.UserId == user.Id && o.Status == "Pending");

            if (order == null)
                return Ok(new { Items = new List<object>(), Total = 0 });

            var result = new
            {
                Items = order.OrderItems.Select(i => new
                {
                    i.ProductId,
                    i.Product.Name,
                    i.Quantity,
                    i.UnitPrice,
                    Total = i.Quantity * i.UnitPrice
                }),
                Total = order.TotalPrice
            };

            return Ok(result);
        }



        [HttpDelete("remove/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var user = await GetCurrentUser();
            if (user == null) return Unauthorized();

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.UserId == user.Id && o.Status == "Pending");

            if (order == null)
                return NotFound("Cart not found");

            var item = order.OrderItems.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
                return NotFound("Item not found in cart");

            order.OrderItems.Remove(item);

           
            order.TotalPrice = order.OrderItems.Sum(i => i.Quantity * i.UnitPrice);

            await _context.SaveChangesAsync();

            return await GetCart();
        
        }


        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout()
        {
            var user = await GetCurrentUser();
            if (user == null) return Unauthorized();

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.UserId == user.Id && o.Status == "Pending");

            if (order == null || !order.OrderItems.Any())
                return BadRequest("Cart is empty");

            order.Status = "Preparing";
            order.OrderDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return await GetCart();
        }














    }
}
