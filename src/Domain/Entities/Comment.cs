using Domain.Abstractions;
using Domain.Common;

namespace Domain.Entities;

public sealed class Comment : BaseEntity, IVotableContent
{
    public required string Description { get; set; }

    public User? User { get; set; }
    
    public Guid UserId { get; set; }

    public ICollection<Vote>? Votes { get; set; }
}