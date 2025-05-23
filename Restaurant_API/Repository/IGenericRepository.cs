namespace Restaurant_API.Repository
{
    public interface IGenericRepository<T> where T:class
    {
        Task<IEnumerable<T>> GetAll();    
        Task<T?> GetById(int id); 
        Task<T>  Add(T entity);
        Task Update(T entity);   
        Task DeleteById(int id);    

    }
}
