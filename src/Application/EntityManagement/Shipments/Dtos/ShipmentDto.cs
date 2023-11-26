#region

using Application.EntityManagement.Addresses.Dtos;
using Domain.Enums;

#endregion

namespace Application.EntityManagement.Shipments.Dtos;

public record ShipmentDto(
    int ExternalId,
    int OrderExternalId,
    Guid TraceId,
    ShipmentStatus ShipmentStatus,
    ShippingMethod ShippingMethod,
    DateTime DateShipped,
    DateTime? DateDelivered,
    decimal ShippingCost,
    AddressDto DestinationAddress,
    AddressDto OriginAddress);