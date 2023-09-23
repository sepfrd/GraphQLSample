using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Shipment : BaseEntity
{
    public Guid TraceId { get; set; }
    public ShipmentStatus Status { get; set; }
    public ShippingMethod ShippingMethod { get; set; }
    public DateTime DateShipped { get; set; }
    public DateTime? DateDelivered { get; set; }
    public decimal ShippingCost { get; set; }
    
    public Guid CarrierId { get; set; }
    public Guid DestinationAddressId { get; set; }
    public Guid OriginAddressId { get; set; }
    public Guid OrderId { get; set; }
}
