using Restaurant_API.Models;

namespace Restaurant_API.Repository
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllWithDetails(); 

        Task<Order?> GetByIdWithDetails(int id);
    }
}
