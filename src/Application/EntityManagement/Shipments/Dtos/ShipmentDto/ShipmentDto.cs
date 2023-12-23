using Application.EntityManagement.Addresses.Dtos.AddressDto;
using Domain.Enums;

namespace Application.EntityManagement.Shipments.Dtos.ShipmentDto;

public record ShipmentDto(
    ShipmentStatus? ShipmentStatus,
    ShippingMethod? ShippingMethod,
    DateTime? DateShipped,
    DateTime? DateDelivered,
    decimal? ShippingCost,
    AddressDto? DestinationAddress,
    AddressDto? OriginAddress);