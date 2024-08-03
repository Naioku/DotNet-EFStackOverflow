namespace EFStackOverflow.Entities;

public class Question
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
    public List<Answer> Answers { get; set; } = new();
    public List<Vote> Votes { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
    public List<Tag> Tags { get; set; } = new();
}