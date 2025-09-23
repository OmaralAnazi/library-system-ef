using LibrarySystem.Data.Entities;
using LibrarySystem.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using LibrarySystem.Services;

namespace LibrarySystem.Controllers;

public interface ICrudController<TCreateDto>
{
    Task<IActionResult> GetAllAsync(CancellationToken ct = default);
    Task<IActionResult> GetByIdAsync(string id, CancellationToken ct = default);
    Task<IActionResult> CreateAsync([FromBody] TCreateDto dto, CancellationToken ct = default);
    Task<IActionResult> DeleteAsync(string id, CancellationToken ct = default);
}

[ApiController]
[Route("api/[controller]")]
public abstract class CrudController<TService, TRepository, TEntity, TResponseDto, TCreateDto>
    : ControllerBase, ICrudController<TCreateDto>
    where TRepository : IRepository<TEntity>
    where TEntity : BaseEntity
    where TService : ICrudService<TRepository, TEntity, TResponseDto, TCreateDto>
{
    protected readonly TService Service;

    protected CrudController(TService service)
    {
        Service = service;
    }

    [HttpGet]
    public virtual async Task<IActionResult> GetAllAsync(CancellationToken ct = default)
    {
        return Ok(await Service.GetAllAsync(ct));
    }

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetByIdAsync(string id, CancellationToken ct = default)
    {
        var dto = await Service.GetByIdAsync(id, ct);
        return Ok(dto);
    }

    [HttpPost]
    public virtual async Task<IActionResult> CreateAsync([FromBody] TCreateDto dto, CancellationToken ct = default)
    {
        var created = await Service.CreateAsync(dto, ct);
        return CreatedAtAction(null, created);
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> DeleteAsync(string id, CancellationToken ct = default)
    {
        await Service.DeleteByIdAsync(id, ct);
        return NoContent();
    }
}
