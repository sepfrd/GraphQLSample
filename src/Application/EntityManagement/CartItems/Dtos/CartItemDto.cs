using Application.EntityManagement.Products.Dtos;

namespace Application.EntityManagement.CartItems.Dtos;

public record CartItemDto
(
    int ExternalId,
    int CartExternalId,
    int Quantity,
    decimal UnitPrice,
    decimal SubTotalPrice,
    ProductDto ProductDto
);