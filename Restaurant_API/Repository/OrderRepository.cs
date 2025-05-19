using Microsoft.EntityFrameworkCore;
using Restaurant_API.Data;
using Restaurant_API.Models;

namespace Restaurant_API.Repository
{
        public class OrderRepository : GenericRepository<Order>, IOrderRepository
        {
            private readonly ApplicationDbContext _context;

            public OrderRepository(ApplicationDbContext context) : base(context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Order>> GetAllWithDetails()
            {
                return await _context.Orders
                    .Include(o => o.User)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .ToListAsync();
            }

            public async Task<Order?> GetByIdWithDetails(int id)
            {
                return await _context.Orders
                    .Include(o => o.User)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .FirstOrDefaultAsync(o => o.Id == id);
            }



        }
}
