using Application.EntityManagement.Addresses.Dtos;
using Domain.Enums;

namespace Application.EntityManagement.Shipments.Dtos;

public record ShipmentDto(
    ShipmentStatus? ShipmentStatus,
    ShippingMethod? ShippingMethod,
    DateTime? DateShipped,
    DateTime? DateDelivered,
    decimal? ShippingCost,
    AddressDto? DestinationAddress,
    AddressDto? OriginAddress);