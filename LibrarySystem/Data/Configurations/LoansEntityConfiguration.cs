using LibrarySystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySystem.Data.Configurations;

public sealed class LoansEntityConfiguration : BaseEntityConfiguration<LoansEntity>
{
    public override void Configure(EntityTypeBuilder<LoansEntity> b)
    {
        base.Configure(b);

        b.Property(x => x.LoanDate).IsRequired();
        b.Property(x => x.ReturnDate).IsRequired(false);

        b.Property(x => x.BookId).IsRequired();
        b.Property(x => x.MemberId).IsRequired();

        b.HasIndex(x => x.BookId);
        b.HasIndex(x => x.MemberId);

        b.HasOne(l => l.Book)
            .WithMany()                // no collection on BooksEntity
            .HasForeignKey(l => l.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(l => l.Member)
            .WithMany()                // no collection on MembersEntity
            .HasForeignKey(l => l.MemberId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}