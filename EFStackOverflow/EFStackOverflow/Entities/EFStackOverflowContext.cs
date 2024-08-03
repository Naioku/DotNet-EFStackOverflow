using Microsoft.EntityFrameworkCore;

namespace EFStackOverflow.Entities;

public class EFStackOverflowContext : DbContext
{
    public EFStackOverflowContext(DbContextOptions options) : base(options) {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
}