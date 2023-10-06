using Domain.Abstractions;
using Domain.Common;

namespace Domain.Entities;

public sealed class Answer : BaseEntity, IVotableContent
{
    public required string Title { get; set; }

    public required string Description { get; set; }

    public Guid UserId { get; set; }

    public Guid QuestionId { get; set; }

    public ICollection<Guid>? VoteIds { get; set; }
}