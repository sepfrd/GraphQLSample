using Domain.Abstractions;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public sealed class Vote : BaseEntity
{
    public VoteType Type { get; set; }

    public Guid UserId { get; set; }

    public Guid ContentId { get; set; }
}