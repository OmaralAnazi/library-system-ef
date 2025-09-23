using LibrarySystem.Data.Entities;
using LibrarySystem.Repositories;

namespace LibrarySystem.Services;

public interface IBooksService
{
    Task TestCreatingNewBook();
}

public class BooksService(IBooksRepository booksRepository) : IBooksService 
{
    public async Task TestCreatingNewBook()
    {
        var bookEntity = new BooksEntity()
        {
            Title = "Book 1",
            Author = "Author 1",
            Isbn = "ISBN 1",
            PublishedAt = DateTime.UtcNow
        };
        
        await booksRepository.AddAsync(bookEntity);
        
    }
}