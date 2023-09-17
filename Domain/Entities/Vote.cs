using Domain.Common;

namespace Domain.Entities;

public class Vote : BaseEntity
{
    public byte Type { get; set; }
}
