using Domain.Entities;

namespace Domain.Interfaces;

public interface IVotableContent
{
    public ICollection<Vote>? Votes { get; set; }
}
