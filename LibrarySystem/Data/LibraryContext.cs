using LibrarySystem.Data.Entities;
using LibrarySystem.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data;

public class LibraryContext(DbContextOptions<LibraryContext> options) : DbContext(options)
{
    public DbSet<BooksEntity> Books { get; set; }
    // public DbSet<Member> Members { get; set; }
    // public DbSet<Loan> Loans { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplySnakeCaseConfiguration();
    }
}