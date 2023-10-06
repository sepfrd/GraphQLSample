using Domain.Abstractions;
using Domain.Common;

namespace Domain.Entities;

public sealed class Comment : BaseEntity, IVotableContent
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public Guid UserId { get; set; }

    public ICollection<Guid>? VoteIds { get; set; }
}