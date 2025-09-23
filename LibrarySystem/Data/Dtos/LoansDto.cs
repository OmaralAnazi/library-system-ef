namespace LibrarySystem.Data.Dtos;

public class LoansDto
{
    public BooksDto Book { get; set; }
    public MembersDto Member { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; } = null;
}