using Microsoft.EntityFrameworkCore;
using MoskBilisimAPI.Context;
using MoskBilisimAPI.Interfaces;
using System.Linq.Expressions;

namespace MoskBilisimAPI.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MoskBilisimDbContext _moskBilisimDbContext;
        public Repository(MoskBilisimDbContext carBookContext)
        {
            _moskBilisimDbContext = carBookContext;
        }
        public async Task CreateAsync(T entity)
        {
            _moskBilisimDbContext.Set<T>().Add(entity);
            await _moskBilisimDbContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _moskBilisimDbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _moskBilisimDbContext.Set<T>().SingleOrDefaultAsync(filter);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _moskBilisimDbContext.Set<T>().FindAsync(id);
        }

        public async Task RemoveAsync(T entity)
        {
            _moskBilisimDbContext.Set<T>().Remove(entity);
            await _moskBilisimDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _moskBilisimDbContext.Set<T>().Update(entity);
            await _moskBilisimDbContext.SaveChangesAsync();
        }
    }
}
