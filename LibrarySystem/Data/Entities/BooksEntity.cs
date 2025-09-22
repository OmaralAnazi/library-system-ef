namespace LibrarySystem.Data.Entities;

public class BooksEntity : BaseEntity
{
    public string Isbn { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime PublishedAt { get; set; }
}