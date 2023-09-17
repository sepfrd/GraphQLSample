using Domain.Common;

namespace Domain.Entities;

public class Review : BaseEntity
{
    public Comment? Comment { get; set; }
}
