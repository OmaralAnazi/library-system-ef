using LibrarySystem.Data.Entities;

namespace LibrarySystem.Data.Dtos;

public class BooksDto(BooksEntity entity) // TODO: change to mapper later
{
    public string Id { get; set; } = entity.Key;
    public string Isbn { get; set; } = entity.Isbn; 
    public string Title { get; set; } = entity.Title;
    public string Author { get; set; } = entity.Author;
    public DateTime PublishedAt { get; set; } = entity.PublishedAt;
}