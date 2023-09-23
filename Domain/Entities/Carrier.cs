using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Carrier : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ICollection<ShippingMethod>? ShippingMethods { get; set; }

    public ICollection<Guid>? ShipmentIds { get; set; }
}
