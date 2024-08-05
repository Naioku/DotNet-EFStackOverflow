using Bogus;
using EFStackOverflow.Entities;

namespace EFStackOverflow;

public static class DataSeeder
{
    public static void Seed(EFStackOverflowContext context)
    {
        if (context.Users.Any()) return;
        
        var faker = new Faker();

        var users = GenerateUsers(faker);
        var questions = GenerateQuestions(faker, users);
        var answers = GenerateAnswers(faker, users, questions);
        var comments = GenerateComments(faker, users, questions, answers);
        var tags = GenerateTags(faker);
        AssignTagsToQuestions(faker, questions, tags);
        var votes = GenerateVotes(faker, users, questions, answers);
        
        context.Users.AddRange(users);
        context.Questions.AddRange(questions);
        context.Answers.AddRange(answers);
        context.Comments.AddRange(comments);
        context.Tags.AddRange(tags);
        context.Votes.AddRange(votes);

        context.SaveChanges();
    }

    private static List<User> GenerateUsers(Faker faker)
    {
        var users = new List<User>();
        for (int i = 0; i < 100; i++)
        {
            users.Add(new User
            {
                Id = Guid.NewGuid(),
                Email = faker.Internet.Email(),
                Password = faker.Internet.Password(),
                DisplayedName = faker.Name.FullName(),
                ProfileImageUrl = faker.Internet.Avatar(),
                Location = faker.Address.City(),
                Title = faker.Name.JobTitle(),
                AboutMe = faker.Lorem.Paragraph()
            });
        }
        return users;
    }

    private static List<Question> GenerateQuestions(Faker faker, List<User> users)
    {
        var questions = new List<Question>();
        for (int i = 0; i < 100; i++)
        {
            var creationDate = faker.Date.Past(5);
            var edited = faker.Random.Bool();
            questions.Add(new Question
            {
                Id = Guid.NewGuid(),
                Title = faker.Lorem.Sentence(),
                Content = faker.Lorem.Paragraphs(),
                OwnerId = users[faker.Random.Int(0, users.Count - 1)].Id,
                CreationDate = creationDate,
                EditorId = edited ? users[faker.Random.Int(0, users.Count - 1)].Id : null,
                EditionDate = edited ? faker.Date.Future(refDate: creationDate) : null,
            });
        }
        return questions;
    }

    private static List<Answer> GenerateAnswers(Faker faker, List<User> users, List<Question> questions)
    {
        var answers = new List<Answer>();
        for (int i = 0; i < 200; i++)
        {
            var question = questions[faker.Random.Int(0, questions.Count - 1)];
            var creationDate = faker.Date.Future(2, question.CreationDate);
            var edited = faker.Random.Bool();
            answers.Add(new Answer
            {
                Id = Guid.NewGuid(),
                Title = faker.Lorem.Sentence(),
                Content = faker.Lorem.Paragraphs(),
                OwnerId = users[faker.Random.Int(0, users.Count - 1)].Id,
                CreationDate = creationDate,
                EditorId = edited ? users[faker.Random.Int(0, users.Count - 1)].Id : null,
                EditionDate = edited ? faker.Date.Future(refDate: creationDate) : null,
                QuestionId = question.Id
            });
        }
        return answers;
    }

    private static List<Comment> GenerateComments(Faker faker, List<User> users, List<Question> questions, List<Answer> answers)
    {
        var comments = new List<Comment>();
        for (int i = 0; i < 300; i++)
        {
            var isQuestion = faker.Random.Bool();
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                Content = faker.Lorem.Sentence(),
                OwnerId = users[faker.Random.Int(0, users.Count - 1)].Id,
            };

            if (isQuestion)
            {
                var question = questions[faker.Random.Int(0, questions.Count - 1)];
                comment.QuestionId = question.Id;
                comment.CreationDate = faker.Date.Future(refDate: question.CreationDate);
            }
            else
            {
                var answer = answers[faker.Random.Int(0, answers.Count - 1)];
                comment.AnswerId = answer.Id;
                comment.CreationDate = faker.Date.Future(refDate: answer.CreationDate);
            }
            
            comments.Add(comment);
        }

        return comments;
    }

    private static List<Tag> GenerateTags(Faker faker)
    {
        var tags = new List<Tag>();
        for (int i = 0; i < 50; i++)
        {
            tags.Add(new Tag
            {
                Name = faker.Lorem.Word()
            });
        }
        return tags;
    }

    private static void AssignTagsToQuestions(Faker faker, List<Question> questions, List<Tag> tags)
    {
        foreach (var question in questions)
        {
            var tagsToAssign = new List<Tag>();
            for (int i = 0; i < faker.Random.Int(1, 5); i++)
            {
                tagsToAssign.Add(tags[faker.Random.Int(0, tags.Count - 1)]);
            }
            question.Tags = tagsToAssign;
        }
    }

    private static List<Vote> GenerateVotes(Faker faker, List<User> users, List<Question> questions, List<Answer> answers)
    {
        var votes = new List<Vote>();
        foreach (var question in questions)
        {
            foreach (var user in users)
            {
                var shouldVote = faker.Random.Bool();
                if (!shouldVote) continue;
                
                votes.Add(new Vote
                {
                    IsUpVote = faker.Random.Bool(),
                    OwnerId = user.Id,
                    QuestionId = question.Id
                });
            }
        }
        
        foreach (var answer in answers)
        {
            foreach (var user in users)
            {
                var shouldVote = faker.Random.Bool();
                if (!shouldVote) continue;
                
                votes.Add(new Vote
                {
                    IsUpVote = faker.Random.Bool(),
                    OwnerId = user.Id,
                    AnswerId = answer.Id
                });
            }
        }

        return votes;
    }
}