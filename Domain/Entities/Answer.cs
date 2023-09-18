using Domain.Common;
using Domain.Entities;

namespace Domain;

public class Answer : BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }

    public Guid UserId { get; set; }
    public Guid QuestionId { get; set; }
    public List<Vote> Votes { get; set; } = new();
}
