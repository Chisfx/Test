using Test.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Test.Infrastructure.DbContexts;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Test.Infrastructure.Repositories
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        public RepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Entities => _dbContext.Set<T>();

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> where)
        {
            return await _dbContext.Set<T>().Where(where).ToListAsync();
        }
        public async Task<List<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> includes)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            query = includes(query);
            return await query.ToListAsync();
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> where, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes)
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(where);
            query = includes(query).AsNoTracking();
            return await query.ToListAsync();
        }
        public async Task<T> GetByIdAsync<TKey>(TKey Id)
        {
            return await _dbContext.Set<T>().FindAsync(Id);
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> where)
        {
            return await _dbContext.Set<T>().AnyAsync(where);
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> where, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            query = includes(query);
            return await query.AnyAsync(where);
        }
        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }
        public async Task<List<T>> AddAsync(List<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            return entities;
        }
        public Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).CurrentValues.SetValues(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
        public async Task UpdateAsync(List<T> entities)
        {
            foreach (var entity in entities)
            {
                await UpdateAsync(entity);
            }
        }
        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }
        public Task DeleteAsync(List<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }
        public async Task<int> CountAsync()
        {
            return await _dbContext.Set<T>().CountAsync();
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>> where)
        {
            return await _dbContext.Set<T>().CountAsync(where);
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> where)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(where);
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> where, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes)
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(where);
            query = includes(query);
            return await query.FirstOrDefaultAsync();
        }
    }
}
