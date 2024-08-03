namespace EFStackOverflow.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public string Content { get; set; }

    public User Owner { get; set; }
    public Guid OwnerId { get; set; }
    public DateTime CreationDate { get; set; }
    
    public Question Question { get; set; }
    public Guid? QuestionId { get; set; }
    
    public Answer Answer { get; set; }
    public Guid? AnswerId { get; set; }
}