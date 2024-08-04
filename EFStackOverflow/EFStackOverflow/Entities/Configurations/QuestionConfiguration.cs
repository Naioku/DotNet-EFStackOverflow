using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFStackOverflow.Entities.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.Property(q => q.Title)
            .HasMaxLength(150)
            .IsRequired();
        builder.Property(q => q.Content)
            .HasMaxLength(8000)
            .IsRequired();
        
        builder.Property(q => q.CreationDate)
            .HasDefaultValueSql("getutcdate()")
            .IsRequired();
        builder.Property(q => q.EditionDate)
            .ValueGeneratedOnUpdate();
        
        builder
            .HasOne(q => q.Owner)
            .WithMany(u => u.OwnedQuestions)
            .HasForeignKey(q => q.OwnerId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder
            .HasOne(q => q.Editor)
            .WithMany(u => u.EditedQuestions)
            .HasForeignKey(q => q.EditorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}