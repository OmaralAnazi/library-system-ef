using LibrarySystem.Data.Entities;

namespace LibrarySystem.Data.Dtos;

public class BooksDto
{
    public string Id { get; set; }
    public string Isbn { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime PublishedAt { get; set; }
}