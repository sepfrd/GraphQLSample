using Domain.Common;

namespace Domain.Entities;

public class Comment : BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    
    public Guid UserId { get; set; }
    public Guid ReviewId { get; set; }
    public List<Vote> Votes { get; set; } = new();
}
