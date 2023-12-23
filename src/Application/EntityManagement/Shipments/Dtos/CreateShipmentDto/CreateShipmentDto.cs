using Domain.Enums;

namespace Application.EntityManagement.Shipments.Dtos.CreateShipmentDto;

public record CreateShipmentDto(
    int OriginAddressExternalId,
    int DestinationAddressExternalId,
    ShippingMethod ShippingMethod,
    DateTime DateToBeShipped,
    DateTime DateToBeDelivered,
    decimal ShippingCost);