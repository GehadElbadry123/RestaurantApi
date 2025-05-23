
using Microsoft.EntityFrameworkCore;
using Restaurant_API.Data;

namespace Restaurant_API.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context; 

        }
        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteById(int id)
        {
             T entity = await _context.Set<T>().FindAsync(id);

            if (entity != null)
            {

                _context.Set<T>().Remove(entity);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();  
        }

        public async Task<T?> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();

        }
    }
}
