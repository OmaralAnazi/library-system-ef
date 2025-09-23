using LibrarySystem.Data.Dtos;
using LibrarySystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBooksService booksService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetBooks(CancellationToken cancellationToken = default)
    {
        return Ok(await booksService.GetAllAsync(cancellationToken));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(string id, CancellationToken cancellationToken = default)
    {
        return Ok(await booksService.GetByIdAsync(id, cancellationToken));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookRequestDto request, CancellationToken cancellationToken = default)
    {
        var created = await booksService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetBookById), new { id = created.Id }, created);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(string id, CancellationToken cancellationToken = default)
    {
        await booksService.DeleteByIdAsync(id, cancellationToken);
        return NoContent();
    }
}