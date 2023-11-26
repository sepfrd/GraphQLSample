#region

using Domain.Entities;

#endregion

namespace Domain.Abstractions;

public interface IVotableContent
{
    public ICollection<Vote>? Votes { get; set; }
}