namespace ChatBoard.Domain.Interface.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}