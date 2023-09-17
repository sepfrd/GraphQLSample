using Domain.Common;

namespace Domain.Entities;

public class Comment : BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    
    public User? User { get; set; }
    public List<Vote>? Votes { get; set; }
    public Review? Review { get; set; }
}
