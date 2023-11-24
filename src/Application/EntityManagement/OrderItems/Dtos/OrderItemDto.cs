using Application.EntityManagement.Products.Dtos;

namespace Application.EntityManagement.OrderItems.Dtos;

public record OrderItemDto(
    decimal UnitPrice,
    int Quantity,
    decimal SubTotalPrice,
    ProductDto Product);