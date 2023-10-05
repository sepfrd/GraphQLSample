using Domain.Abstractions;
using Domain.Common;

namespace Domain.Entities;

public sealed class Favorite : BaseEntity
{
    public User? User { get; set; }

    public IVotableContent? Content { get; set; }
}