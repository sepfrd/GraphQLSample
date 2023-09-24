using Domain.Common;
using Domain.Interfaces;

namespace Domain.Entities;

public class Comment : BaseEntity, IVotableContent
{
    public string? Title { get; set; }
    public string? Description { get; set; }

    public User? User { get; set; }
    public ICollection<Vote>? Votes { get; set; }
}
