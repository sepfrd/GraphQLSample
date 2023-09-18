using Domain.Common;

namespace Domain.Entities;

public class Review : BaseEntity
{
    public Guid CommentId { get; set; }
}
