namespace EFStackOverflow.Entities;

public class QuestionTag
{
    public Question Question { get; set; }
    public Guid QuestionId { get; set; }
    
    public Tag Tag { get; set; }
    public int TagId { get; set; }
}