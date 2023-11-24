namespace Application.EntityManagement.OrderItems.Dtos;

public record CreateOrderItemDto(
    int ProductExternalId,
    decimal UnitPrice,
    decimal SubTotalPrice,
    int Quantity);