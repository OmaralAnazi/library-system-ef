using System.Linq.Expressions;
using LibrarySystem.Data;
using LibrarySystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Repositories;

public abstract class BaseRepository<TEntity>(LibraryContext context) : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, object?>>>? includes = null, bool asTracking = false,
        CancellationToken cancellationToken = default)
    {
        var query = asTracking ? _dbSet : _dbSet.AsNoTracking();

        // Apply includes if provided
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(bool asTracking = false, CancellationToken cancellationToken = default)
    {
        var query = asTracking ? _dbSet : _dbSet.AsNoTracking();
        
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(int id, bool asTracking = false, CancellationToken cancellationToken = default)
    {
        var query = asTracking ? _dbSet : _dbSet.AsNoTracking();

        return await query.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(string id, bool asTracking = false, CancellationToken cancellationToken = default)
    {
        var query = asTracking ? _dbSet : _dbSet.AsNoTracking();

        return await query.FirstOrDefaultAsync(e => e.Key == id, cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, true, cancellationToken) ?? throw new KeyNotFoundException();
        entity.DeletedAt = DateTime.UtcNow;
        _dbSet.Update(entity);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, true, cancellationToken) ?? throw new KeyNotFoundException();
        entity.DeletedAt = DateTime.UtcNow;
        _dbSet.Update(entity);
    }
    
    public void Delete(TEntity entity)
    {
        entity.DeletedAt = DateTime.UtcNow;
        _dbSet.Update(entity);
    }
}