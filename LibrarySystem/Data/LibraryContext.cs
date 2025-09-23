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
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplySnakeCaseConfiguration();
        modelBuilder.ApplyGlobalSoftDeleteFilter();
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;

        foreach (var e in ChangeTracker.Entries<BaseEntity>())
        {
            if (e.State == EntityState.Added)
                e.Entity.CreatedAt = now;

            if (e.State == EntityState.Modified)
                e.Entity.UpdatedAt = now;

            if (e.Entity.DeletedAt is not null)
                e.State = EntityState.Modified; // soft-delete stays an UPDATE
        }
        
        return base.SaveChangesAsync(ct);
    }
}