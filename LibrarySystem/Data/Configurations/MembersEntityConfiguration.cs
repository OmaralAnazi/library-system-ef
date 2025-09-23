using LibrarySystem.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySystem.Data.Configurations;

public sealed class MembersEntityConfiguration : BaseEntityConfiguration<MembersEntity>
{
    public override void Configure(EntityTypeBuilder<MembersEntity> b)
    {
        base.Configure(b);

        b.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        b.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(320); // RFC max

        b.HasIndex(x => x.Email).IsUnique();
    }
}