namespace LibrarySystem.Data.Entities;

public class LoansEntity : BaseEntity
{
    public long BookId { get; set; }
    public long MemberId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    // Navigation Objects
    public BooksEntity Book { get; set; }
    public MembersEntity Member { get; set; }
}