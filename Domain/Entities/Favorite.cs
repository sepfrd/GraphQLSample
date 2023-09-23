using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Favorite : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid ContentId { get; set; }
    public ContentType ContentType { get; set; }
}
