using ChatBoard.DataBase.Context;
using ChatBoard.DataBase.Interface;
using Microsoft.EntityFrameworkCore;

namespace ChatBoard.DataBase.Repository
{
    internal class BaseRepository<T>(DataBaseContext context) : IBaseRepository<T> where T : class
    {
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}