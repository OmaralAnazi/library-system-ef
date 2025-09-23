using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Data.Dtos;

public class CreateLoanRequestDto
{
    [Required]
    public string BookId { get; set; }
    
    [Required]
    public string MemberId { get; set; }

    public DateTime? ReturnDate { get; set; } = null;
}