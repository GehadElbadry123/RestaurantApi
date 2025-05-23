using Restaurant_API.Models;

namespace Restaurant_API.Repository
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllWithProductCount();

        Task<Category?> GetByIdWithProductCount(int id);
    }
}
