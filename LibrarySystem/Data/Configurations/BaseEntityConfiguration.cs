using LibrarySystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySystem.Data.Configurations;

public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> b)
    {
        b.HasKey(e => e.Id);
        b.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        b.Property(e => e.CreatedAt)
            .IsRequired();

        b.Property(e => e.UpdatedAt)
            .IsRequired();

        b.Property(e => e.DeletedAt);

        b.Property(e => e.Key)
            .IsRequired()
            .HasMaxLength(64);

        b.HasIndex(e => e.Key)
            .IsUnique();
    }
}