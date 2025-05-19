using Microsoft.EntityFrameworkCore;
using Restaurant_API.Data;
using Restaurant_API.Models;

namespace Restaurant_API.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Product>> GetAllWithCategory()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }


        public async Task<Product?> GetByIdWithCategory(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p=> p.Id == id);   
        }

    }
}
