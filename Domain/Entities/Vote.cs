using Domain.Common;

namespace Domain.Entities;

public class Vote : BaseEntity
{
    public VoteType Type { get; set; }
    public ContentType ContentType { get; set; }

    public Guid UserId { get; set; }
    public Guid ContentId { get; set; }
}
