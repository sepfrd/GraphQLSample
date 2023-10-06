using Domain.Common;

namespace Domain.Entities;

public sealed class Order : BaseEntity
{
    public decimal TotalPrice { get; set; }

    public Guid PaymentId { get; set; }

    public Guid ShipmentId { get; set; }

    public ICollection<Guid>? OrderItemIds { get; set; }
}