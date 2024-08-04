using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFStackOverflow.Entities.Configurations;

public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
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
            .HasOne(a => a.Owner)
            .WithMany(u => u.OwnedAnswers)
            .HasForeignKey(a => a.OwnerId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder
            .HasOne(a => a.Editor)
            .WithMany(u => u.EditedAnswers)
            .HasForeignKey(a => a.EditorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(a => a.Question)
            .WithMany(q => q.Answers)
            .HasForeignKey(a => a.QuestionId);
    }
}