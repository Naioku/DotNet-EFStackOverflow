using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFStackOverflow.Entities.Configurations;

public class VoteConfiguration : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder
            .HasOne(v => v.Owner)
            .WithMany(u => u.Votes)
            .HasForeignKey(v => v.OwnerId)
            .IsRequired();
        
        builder
            .HasOne(v => v.Question)
            .WithMany(q => q.Votes)
            .HasForeignKey(v => v.QuestionId);
        builder
            .HasOne(v => v.Answer)
            .WithMany(a => a.Votes)
            .HasForeignKey(v => v.AnswerId);
    }
}