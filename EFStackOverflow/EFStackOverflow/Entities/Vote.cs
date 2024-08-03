namespace EFStackOverflow.Entities;

public class Vote
{
    public int Id { get; set; }
    public bool IsUpVote { get; set; }

    public User Owner { get; set; }
    public Guid OwnerId { get; set; }
    public Question Question { get; set; }
    public Guid? QuestionId { get; set; }
    public Answer Answer { get; set; }
    public Guid? AnswerId { get; set; }
}