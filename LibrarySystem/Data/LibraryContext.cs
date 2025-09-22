using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data;

public class LibraryContext(DbContextOptions<LibraryContext> options) : DbContext(options)
{
    // public DbSet<Book> Books { get; set; }
    // public DbSet<Member> Members { get; set; }
    // public DbSet<Loan> Loans { get; set; }
}