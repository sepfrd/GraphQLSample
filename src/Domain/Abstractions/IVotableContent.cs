using Domain.Entities;

namespace Domain.Abstractions;

public interface IVotableContent
{
    public ICollection<Guid>? VoteIds { get; set; }
}