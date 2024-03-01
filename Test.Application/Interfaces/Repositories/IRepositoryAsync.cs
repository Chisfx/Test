using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
namespace Test.Application.Interfaces.Repositories
{
    public interface IRepositoryAsync<T> where T : class
    {
        IQueryable<T> Entities { get; }
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> where);
        Task<List<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> includes);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> where, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes);
        Task<T> GetByIdAsync<TKey>(TKey Id);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> where);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> where, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes);
        Task<bool> AnyAsync(Expression<Func<T, bool>> where);
        Task<bool> AnyAsync(Expression<Func<T, bool>> where, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes);
        Task<T> AddAsync(T entity);
        Task<List<T>> AddAsync(List<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateAsync(List<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteAsync(List<T> entities);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> where);
    }
}
