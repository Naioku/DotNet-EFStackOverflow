using Bogus;
using EFStackOverflow.Entities;

namespace EFStackOverflow;

public class BogusSeeder : IDataSeeder
{
    private readonly Faker _faker = new();
    
    public void Seed(EFStackOverflowContext context)
    {
        if (context.Users.Any()) return;
        
        var users = GenerateUsers();
        var questions = GenerateQuestions(users);
        var answers = GenerateAnswers(users, questions);
        var comments = GenerateComments(users, questions, answers);
        var tags = GenerateTags();
        AssignTagsToQuestions(questions, tags);
        var votes = GenerateVotes(users, questions, answers);
        
        context.Users.AddRange(users);
        context.Questions.AddRange(questions);
        context.Answers.AddRange(answers);
        context.Comments.AddRange(comments);
        context.Tags.AddRange(tags);
        context.Votes.AddRange(votes);

        context.SaveChanges();
    }

    private List<User> GenerateUsers()
    {
        var users = new List<User>();
        for (int i = 0; i < 100; i++)
        {
            users.Add(new User
            {
                Id = Guid.NewGuid(),
                Email = _faker.Internet.Email(),
                Password = _faker.Internet.Password(),
                DisplayedName = _faker.Name.FullName(),
                ProfileImageUrl = _faker.Internet.Avatar(),
                Location = _faker.Address.City(),
                Title = _faker.Name.JobTitle(),
                AboutMe = _faker.Lorem.Paragraph()
            });
        }
        return users;
    }

    private List<Question> GenerateQuestions(List<User> users)
    {
        var questions = new List<Question>();
        for (int i = 0; i < 100; i++)
        {
            var creationDate = _faker.Date.Past(5);
            var edited = _faker.Random.Bool();
            questions.Add(new Question
            {
                Id = Guid.NewGuid(),
                Title = _faker.Lorem.Sentence(),
                Content = _faker.Lorem.Paragraphs(),
                OwnerId = users[_faker.Random.Int(0, users.Count - 1)].Id,
                CreationDate = creationDate,
                EditorId = edited ? users[_faker.Random.Int(0, users.Count - 1)].Id : null,
                EditionDate = edited ? _faker.Date.Future(refDate: creationDate) : null,
            });
        }
        return questions;
    }

    private List<Answer> GenerateAnswers(List<User> users, List<Question> questions)
    {
        var answers = new List<Answer>();
        for (int i = 0; i < 200; i++)
        {
            var question = questions[_faker.Random.Int(0, questions.Count - 1)];
            var creationDate = _faker.Date.Future(2, question.CreationDate);
            var edited = _faker.Random.Bool();
            answers.Add(new Answer
            {
                Id = Guid.NewGuid(),
                Title = _faker.Lorem.Sentence(),
                Content = _faker.Lorem.Paragraphs(),
                OwnerId = users[_faker.Random.Int(0, users.Count - 1)].Id,
                CreationDate = creationDate,
                EditorId = edited ? users[_faker.Random.Int(0, users.Count - 1)].Id : null,
                EditionDate = edited ? _faker.Date.Future(refDate: creationDate) : null,
                QuestionId = question.Id
            });
        }
        return answers;
    }

    private List<Comment> GenerateComments(List<User> users, List<Question> questions, List<Answer> answers)
    {
        var comments = new List<Comment>();
        for (int i = 0; i < 300; i++)
        {
            var isQuestion = _faker.Random.Bool();
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                Content = _faker.Lorem.Sentence(),
                OwnerId = users[_faker.Random.Int(0, users.Count - 1)].Id,
            };

            if (isQuestion)
            {
                var question = questions[_faker.Random.Int(0, questions.Count - 1)];
                comment.QuestionId = question.Id;
                comment.CreationDate = _faker.Date.Future(refDate: question.CreationDate);
            }
            else
            {
                var answer = answers[_faker.Random.Int(0, answers.Count - 1)];
                comment.AnswerId = answer.Id;
                comment.CreationDate = _faker.Date.Future(refDate: answer.CreationDate);
            }
            
            comments.Add(comment);
        }

        return comments;
    }

    private List<Tag> GenerateTags()
    {
        var tags = new List<Tag>();
        for (int i = 0; i < 50; i++)
        {
            tags.Add(new Tag
            {
                Name = _faker.Lorem.Word()
            });
        }
        return tags;
    }

    private void AssignTagsToQuestions(List<Question> questions, List<Tag> tags)
    {
        foreach (var question in questions)
        {
            var tagsToAssign = new List<Tag>();
            for (int i = 0; i < _faker.Random.Int(1, 5); i++)
            {
                tagsToAssign.Add(tags[_faker.Random.Int(0, tags.Count - 1)]);
            }
            question.Tags = tagsToAssign;
        }
    }

    private List<Vote> GenerateVotes(List<User> users, List<Question> questions, List<Answer> answers)
    {
        var votes = new List<Vote>();
        foreach (var question in questions)
        {
            foreach (var user in users)
            {
                var shouldVote = _faker.Random.Bool();
                if (!shouldVote) continue;
                
                votes.Add(new Vote
                {
                    IsUpVote = _faker.Random.Bool(),
                    OwnerId = user.Id,
                    QuestionId = question.Id
                });
            }
        }
        
        foreach (var answer in answers)
        {
            foreach (var user in users)
            {
                var shouldVote = _faker.Random.Bool();
                if (!shouldVote) continue;
                
                votes.Add(new Vote
                {
                    IsUpVote = _faker.Random.Bool(),
                    OwnerId = user.Id,
                    AnswerId = answer.Id
                });
            }
        }

        return votes;
    }
}