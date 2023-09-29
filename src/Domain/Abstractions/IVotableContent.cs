using Domain.Entities;

namespace Domain.Abstractions;

public interface IVotableContent
{
    public ICollection<Vote>? Votes { get; set; }
}
