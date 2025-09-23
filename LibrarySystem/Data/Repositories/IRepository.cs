using System.Linq.Expressions;
using LibrarySystem.Data.Entities;

namespace LibrarySystem.Data.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object?>>>? includes = null, bool asTracking = false, CancellationToken cancellationToken = default);
    Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object?>>>? includes = null, bool asTracking = false, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllAsync(bool asTracking = false, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(int id, bool asTracking = false, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(string id, bool asTracking = false, CancellationToken cancellationToken = default);
    
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    void Delete(TEntity entity);

}