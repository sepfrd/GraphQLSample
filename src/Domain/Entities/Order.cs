#region

using Domain.Common;

#endregion

namespace Domain.Entities;

public sealed class Order : BaseEntity
{
    public decimal TotalPrice { get; set; }

    public User? User { get; set; }

    public Guid UserId { get; set; }

    public Payment? Payment { get; set; }

    public Guid PaymentId { get; set; }

    public Shipment? Shipment { get; set; }

    public Guid ShipmentId { get; set; }

    public ICollection<OrderItem>? OrderItems { get; set; }
}