using ChatBoard.Domain.Interface.Repository;
using ChatBoard.Infrastructure.DataBase.Context;
using Microsoft.EntityFrameworkCore;

namespace ChatBoard.Infrastructure.DataBase.Repository
{
    public abstract class BaseRepository<T>(DataBaseContext context) : IBaseRepository<T> where T : class
    {
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}