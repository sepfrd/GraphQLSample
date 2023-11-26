using Domain.Enums;

namespace Application.EntityManagement.Shipments.Dtos;

public record CreateShipmentDto(
    int OriginAddressExternalId,
    int DestinationAddressExternalId,
    ShippingMethod ShippingMethod,
    DateTime DateToBeShipped,
    DateTime DateToBeDelivered,
    decimal ShippingCost);