using EFStackOverflow.Entities;

namespace EFStackOverflow;

public interface IDataSeeder
{
    void Seed(EFStackOverflowContext context);
}