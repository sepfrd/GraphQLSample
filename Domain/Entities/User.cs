using Domain.Common;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public int Score { get; set; }

    public Guid? PersonId { get; set; }
    public List<Question> Questions { get; set; } = new();
    public List<Answer> Answers { get; set; } = new();
    public List<Vote> Votes { get; set; } = new();
    public List<Favorite> Favorites { get; set; } = new();
}
