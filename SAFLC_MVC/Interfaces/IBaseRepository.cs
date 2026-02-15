namespace SAFLC_MVC.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();

        Task<T?> GetByIdAsync(object id);

        Task SaveAsync(T entity);

        void Delete(T entity);
    }
}
