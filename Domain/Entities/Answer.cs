using Domain.Common;

namespace Domain.Entities;

public class Answer : BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }

    public Guid UserId { get; set; }
    public Guid QuestionId { get; set; }
    public ICollection<Guid>? VoteIds { get; set; }
}
