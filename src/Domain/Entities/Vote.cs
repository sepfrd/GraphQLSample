using Domain.Common;
using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities;

public class Vote : BaseEntity
{
    public VoteType Type { get; set; }

    public User? User { get; set; }
    public IVotableContent? Content { get; set; }
}