namespace Application.EntityManagement.OrderItems.Dtos.CreateOrderItemDto;

public record CreateOrderItemDto(
    int ProductExternalId,
    decimal UnitPrice,
    int Quantity);