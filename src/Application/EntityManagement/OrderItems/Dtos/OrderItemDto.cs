using Application.EntityManagement.Products.Dtos;

namespace Application.EntityManagement.OrderItems.Dtos;

public record OrderItemDto(
    int ExternalId,
    decimal UnitPrice,
    int Quantity,
    decimal SubTotalPrice,
    ProductDto Product);