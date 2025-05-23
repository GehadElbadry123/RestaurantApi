using Restaurant_API.Models;

namespace Restaurant_API.Repository
{
    public interface IOrderItemRepository : IGenericRepository<OrderItem>
    {
        Task<IEnumerable<OrderItem>> GetAllWithProduct();
        Task<OrderItem?> GetByIdWithProduct(int id);


    }  
}
