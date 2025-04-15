using System.Linq.Expressions;

namespace WarehouseOptimizer.Infrastructure;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, bool asNoTracking = false);
    Task<IEnumerable<T>> GetAllAsync(bool asNoTracking = false);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false);
    Task AddAsync(T entity);
}