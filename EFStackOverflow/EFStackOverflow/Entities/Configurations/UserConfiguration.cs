using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFStackOverflow.Entities.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(e => e.Email)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(e => e.Password)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(e => e.DisplayedName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.ProfileImageUrl)
            .HasMaxLength(2048);

        builder.Property(e => e.Location)
            .HasMaxLength(100);

        builder.Property(e => e.Title)
            .HasMaxLength(100);

        builder.Property(e => e.AboutMe)
            .HasMaxLength(1000);

        
        builder.HasMany(u => u.Votes).WithOne(v => v.Owner).HasForeignKey(v => v.OwnerId);
    }
}