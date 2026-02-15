using Microsoft.EntityFrameworkCore;
using SAFLC_MVC.Data;
using SAFLC_MVC.Interfaces;

namespace SAFLC_MVC.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly SaflcDbContext _context;

        protected readonly DbSet<T> _dbSet;

        public BaseRepository(SaflcDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task SaveAsync(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                await _dbSet.AddAsync(entity);
            }
            else
            {
                _dbSet.Update(entity);
            }
            await _context.SaveChangesAsync();
        }

        public async Task BatchSaveAsync(List<T> entities)
        {
            foreach (var entity in entities)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    await _dbSet.AddAsync(entity);
                }
                else
                {
                    _dbSet.Update(entity);
                }
            }
            await _context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}
