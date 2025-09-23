using AutoMapper;
using LibrarySystem.Data;
using LibrarySystem.Data.Entities;
using LibrarySystem.Repositories;

namespace LibrarySystem.Services;

public interface ICrudService<TRepository, TEntity, TResponseDto, TCreateDto>
    where TRepository : IRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<List<TResponseDto>> GetAllAsync(CancellationToken ct = default);
    Task<TResponseDto> GetByIdAsync(string id, CancellationToken ct = default);
    Task<TResponseDto> CreateAsync(TCreateDto request, CancellationToken ct = default);
    Task DeleteByIdAsync(string id, CancellationToken ct = default);
}

public abstract class CrudService<TRepository, TEntity, TResponseDto, TCreateDto>
    : ICrudService<TRepository, TEntity, TResponseDto, TCreateDto>
    where TRepository : IRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly TRepository _repo;
    private readonly IDbUnitOfWork _uow;
    private readonly IMapper _mapper;

    protected CrudService(TRepository repo, IDbUnitOfWork unitOfWork, IMapper mapper) 
        => (_repo, _uow, _mapper) = (repo, unitOfWork, mapper);

    public virtual async Task<List<TResponseDto>> GetAllAsync(CancellationToken ct = default)
    {
        var entities = await _repo.GetAllAsync(false, ct);
        return _mapper.Map<List<TResponseDto>>(entities);
    }

    public virtual async Task<TResponseDto> GetByIdAsync(string id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, false, ct)
                    ?? throw new KeyNotFoundException($"Entity '{typeof(TEntity).Name}' with key '{id}' not found.");
        return _mapper.Map<TResponseDto>(entity);
    }

    public virtual async Task<TResponseDto> CreateAsync(TCreateDto request, CancellationToken ct = default)
    {
        var entity = _mapper.Map<TEntity>(request);
        await _repo.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
        return _mapper.Map<TResponseDto>(entity);
    }

    public virtual async Task DeleteByIdAsync(string id, CancellationToken ct = default)
    {
        await _repo.DeleteAsync(id, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
