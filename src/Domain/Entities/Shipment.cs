#region

using Domain.Common;
using Domain.Enums;

#endregion

namespace Domain.Entities;

public sealed class Shipment : BaseEntity
{
    public Guid TraceId { get; } = Guid.NewGuid();

    public ShipmentStatus ShipmentStatus { get; set; } = ShipmentStatus.Pending;

    public ShippingMethod ShippingMethod { get; set; }

    public DateTime DateToBeShipped { get; set; }

    public DateTime? DateToBeDelivered { get; set; }

    public decimal ShippingCost { get; set; }

    public Address? DestinationAddress { get; set; }

    public Guid DestinationAddressId { get; set; }

    public Address? OriginAddress { get; set; }

    public Guid OriginAddressId { get; set; }

    public Order? Order { get; set; }

    public Guid OrderId { get; set; }
}