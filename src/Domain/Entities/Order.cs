using Domain.Common;

namespace Domain.Entities;

public sealed class Order : BaseEntity
{
    public decimal TotalPrice { get; set; }
    public Payment? Payment { get; set; }
    public Shipment? Shipment { get; set; }

    public ICollection<OrderItem>? OrderItems { get; set; }
}
