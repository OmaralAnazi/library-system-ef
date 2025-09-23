using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Data.Dtos;

public class CreateMemberRequestDto
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Email { get; set; }
}