using LibrarySystem.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySystem.Data.Configurations;

public sealed class BooksEntityConfiguration : BaseEntityConfiguration<BooksEntity>
{
    public override void Configure(EntityTypeBuilder<BooksEntity> b)
    {
        base.Configure(b);

        b.Property(x => x.Isbn)
            .IsRequired()
            .HasMaxLength(32);

        b.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(256);

        b.Property(x => x.Author)
            .IsRequired()
            .HasMaxLength(256);

        b.Property(x => x.PublishedAt)
            .IsRequired();
    }
}