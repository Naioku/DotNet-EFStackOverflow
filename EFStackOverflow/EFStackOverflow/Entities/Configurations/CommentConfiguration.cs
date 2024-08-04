using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFStackOverflow.Entities.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(c => c.Content)
            .HasMaxLength(600)
            .IsRequired();
        
        builder.Property(c => c.CreationDate)
            .HasDefaultValueSql("getutcdate()")
            .IsRequired();
        
        builder
            .HasOne(c => c.Owner)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.OwnerId)
            .IsRequired();
        
        builder
            .HasOne(c => c.Question)
            .WithMany(q => q.Comments)
            .HasForeignKey(c => c.QuestionId);
        
        builder
            .HasOne(c => c.Answer)
            .WithMany(a => a.Comments)
            .HasForeignKey(c => c.AnswerId);
    }
}