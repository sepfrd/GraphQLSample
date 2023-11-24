namespace Application.EntityManagement.OrderItems.Dtos;

public record CreateOrderItemDto(
    int OrderExternalId,
    int ProductExternalId,
    decimal UnitPrice,
    decimal SubTotalPrice,
    int Quantity);