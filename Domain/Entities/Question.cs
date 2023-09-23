using Domain.Common;

namespace Domain.Entities;

public class Question : BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }

    public Guid UserId { get; set; }
    public ICollection<Guid>? AnswerIds { get; set; }
    public ICollection<Guid>? VoteIds { get; set; }
}
