using Microsoft.EntityFrameworkCore;

namespace EFStackOverflow.Entities;

public class EFStackOverflowContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Vote> Votes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<QuestionTag> QuestionTags { get; set; }
    
    public EFStackOverflowContext(DbContextOptions options) : base(options) {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
}