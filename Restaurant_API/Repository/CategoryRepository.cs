using Microsoft.EntityFrameworkCore;
using Restaurant_API.Data;
using Restaurant_API.Models;

namespace Restaurant_API.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {

        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task<IEnumerable<Category>> GetAllWithProductCount()
        {
            return await _context.Categories.Include(c => c.Products).ToListAsync();
        }

        public async Task<Category?> GetByIdWithProductCount(int id)
        {
            return await _context.Categories
               .Include(c => c.Products)
               .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
