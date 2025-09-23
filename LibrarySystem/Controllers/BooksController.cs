using LibrarySystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBooksService booksService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(string id)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBook()
    {
        await booksService.TestCreatingNewBook();
        return Ok();
    }
}