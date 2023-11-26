namespace Application.EntityManagement.OrderItems.Dtos;

public record OrderItemDto(
    int ExternalId,
    int ProductExternalId,
    decimal UnitPrice,
    int Quantity,
    decimal SubTotalPrice);