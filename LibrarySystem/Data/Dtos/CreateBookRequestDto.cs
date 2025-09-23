using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Data.Dtos;

public class CreateBookRequestDto
{
    [Required]
    public string Isbn { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Author { get; set; }
    
    [Required]
    public DateTime PublishedAt { get; set; }
}