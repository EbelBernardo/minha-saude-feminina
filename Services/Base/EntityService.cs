using Microsoft.EntityFrameworkCore;
using MinhaSaudeFeminina.Data;
using MinhaSaudeFeminina.Services.Interfaces;
using System.Runtime.CompilerServices;

namespace MinhaSaudeFeminina.Services.Base
{
    public abstract class EntityService<T> : IEntityService<T>
        where T : class
    {
        protected readonly AppDbContext _context;
        protected EntityService(AppDbContext context)
            => _context = context;

        public virtual async Task<bool> ExistsAsync(int id)
            => await _context.Set<T>().FindAsync(id) != null;

        public virtual async Task<T?> GetByIdAsync(int id)
            => await _context.Set<T>().FindAsync(id);

        public virtual async Task<List<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();

        public virtual async Task<T> CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
