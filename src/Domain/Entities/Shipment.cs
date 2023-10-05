using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public sealed class Shipment : BaseEntity
{
    public Guid TraceId { get; set; }

    public ShipmentStatus Status { get; set; }

    public ShippingMethod ShippingMethod { get; set; }

    public DateTime DateShipped { get; set; }

    public DateTime? DateDelivered { get; set; }

    public decimal ShippingCost { get; set; }

    public Address? DestinationAddress { get; set; }

    public Address? OriginAddress { get; set; }

    public Order? Order { get; set; }
}