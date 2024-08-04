using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFStackOverflow.Entities.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(t => t.Name)
            .HasMaxLength(35)
            .IsRequired();
        
        builder
            .HasMany(t => t.Questions)
            .WithMany(q => q.Tags)
            .UsingEntity<QuestionTag>(
                qtBuilder => qtBuilder.HasOne(qt => qt.Question).WithMany().HasForeignKey(qt => qt.QuestionId),
                qtBuilder => qtBuilder.HasOne(qt => qt.Tag).WithMany().HasForeignKey(qt => qt.TagId),
                qtBuilder =>
                {
                    qtBuilder.HasKey(qt => new { qt.QuestionId, qt.TagId });
                });
    }
}