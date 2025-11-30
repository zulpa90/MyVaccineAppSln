using System.Linq.Expressions;

namespace MyVaccine.WebApi.Repositories.Contracts;

public interface IBaseRepository<T>
{
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(List<T> entity);
    Task UpdateAsync(T entity);
    Task UpdateRangeAsync(List<T> entity);
    Task DeleteAsync(T entity);
    Task DeleteRangeAsync(List<T> entity);
    IQueryable<T> GetAll();
    Task<List<T>> GetAllAsync();
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
    IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
    Task PatchAsync(T entity);
    Task PatchRangeAsync(List<T> entities);
    IQueryable<T> FindByAsNoTracking(Expression<Func<T, bool>> predicate);
}
