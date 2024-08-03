namespace EFStackOverflow.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string DisplayedName { get; set; }
    public string ProfileImageUrl { get; set; }
    public string Location { get; set; }
    public string Title { get; set; }
    public string AboutMe { get; set; }

    public List<Question> OwnedQuestions { get; set; } = new();
    public List<Question> EditedQuestions { get; set; } = new();
    public List<Answer> OwnedAnswers { get; set; } = new();
    public List<Answer> EditedAnswers { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
    public List<Vote> Votes { get; set; } = new();
}