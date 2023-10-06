using Domain.Abstractions;
using Domain.Common;

namespace Domain.Entities;

public sealed class Question : BaseEntity, IVotableContent
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public Guid UserId { get; set; }

    public ICollection<Answer>? Answers { get; set; }

    public ICollection<Guid>? VoteIds { get; set; }
}