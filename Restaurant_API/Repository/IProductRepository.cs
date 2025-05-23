using Restaurant_API.Models;

namespace Restaurant_API.Repository
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllWithCategory();

        Task<Product?> GetByIdWithCategory(int id);
    }
}
