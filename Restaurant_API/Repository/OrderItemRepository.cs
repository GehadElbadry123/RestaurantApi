using Microsoft.EntityFrameworkCore;
using Restaurant_API.Data;
using Restaurant_API.Models;

namespace Restaurant_API.Repository
{
        public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
        {
            private readonly ApplicationDbContext _context;

            public OrderItemRepository(ApplicationDbContext context) : base(context)
            {
                _context = context;
            }

            public async Task<IEnumerable<OrderItem>> GetAllWithProduct()
            {
                return await _context.OrderItems
                    .Include(oi => oi.Product)
                    .Include(oi => oi.Order)
                    .ToListAsync();
            }

            public async Task<OrderItem?> GetByIdWithProduct(int id)
            {
                return await _context.OrderItems
                    .Include(oi => oi.Product)
                    .Include(oi => oi.Order)
                    .FirstOrDefaultAsync(oi => oi.Id == id);
            }

        }
}
