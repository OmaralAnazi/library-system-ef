using LibrarySystem.Data;
using LibrarySystem.Data.Dtos;
using LibrarySystem.Data.Entities;
using LibrarySystem.Repositories;

namespace LibrarySystem.Services;

public interface IBooksService
{
    Task<IEnumerable<BooksDto>> GetAllBooksAsync(CancellationToken cancellationToken = default);
    Task<BooksDto> GetBookByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<BooksDto> CreateBookAsync(CreateBookRequestDto request, CancellationToken cancellationToken = default);
    Task DeleteBookByIdAsync(string id, CancellationToken cancellationToken = default);
}

public class BooksService(IDbUnitOfWork dbUnitOfWork, IBooksRepository booksRepository) : IBooksService 
{
    public async Task<IEnumerable<BooksDto>> GetAllBooksAsync(CancellationToken cancellationToken = default)
    {
        var books = await booksRepository.GetAllAsync(false, cancellationToken);
        var dtos = books.Select(b => new BooksDto(b));
        return dtos;
    }

    public async Task<BooksDto> GetBookByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var book = await booksRepository.GetByIdAsync(id, false, cancellationToken) ?? throw new KeyNotFoundException();
        var dtos = new BooksDto(book);
        return dtos;
    }

    public async Task<BooksDto> CreateBookAsync(CreateBookRequestDto request, CancellationToken cancellationToken = default)
    {
        var newBook = new BooksEntity()
        {
            Title = request.Title,
            Author = request.Author,
            Isbn = request.Isbn,
            PublishedAt = request.PublishedAt
        };
        
        await booksRepository.AddAsync(newBook, cancellationToken);
        await dbUnitOfWork.SaveChangesAsync(cancellationToken);
        
        return new BooksDto(newBook);
    }

    public async Task DeleteBookByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var book = await booksRepository.GetByIdAsync(id, true, cancellationToken) ?? throw new KeyNotFoundException();
        booksRepository.Delete(book);
        await dbUnitOfWork.SaveChangesAsync(cancellationToken);
    }
}