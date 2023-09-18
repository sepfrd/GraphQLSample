using Domain.Common;

namespace Domain;

public class Favorite : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid ContentId { get; set; }
    public ContentType ContentType { get; set; }
}
