namespace EFStackOverflow.Entities;

public class Answer
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public User Owner { get; set; }
    public Guid? OwnerId { get; set; }
    public DateTime CreationDate { get; set; }
    public User Editor { get; set; }
    public Guid? EditorId { get; set; }
    public DateTime? EditionDate { get; set; }
    public Question Question { get; set; }
    public Guid QuestionId { get; set; }
    public List<Comment> Comments { get; set; } = new();
    public List<Vote> Votes { get; set; } = new();
}