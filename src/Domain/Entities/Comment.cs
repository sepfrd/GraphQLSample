#region

using Domain.Abstractions;
using Domain.Common;

#endregion

namespace Domain.Entities;

public sealed class Comment : BaseEntity, IVotableContent
{
    public required string Description { get; set; }

    public User? User { get; set; }

    public Guid UserId { get; set; }

    public Product? Product { get; set; }

    public Guid ProductId { get; set; }

    public ICollection<Vote>? Votes { get; set; }
}