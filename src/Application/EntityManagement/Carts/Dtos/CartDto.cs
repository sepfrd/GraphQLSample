using Application.EntityManagement.CartItems.Dtos;

namespace Application.EntityManagement.Carts.Dtos;

public record CartDto(
    int UserExternalId,
    ICollection<CartItemDto>? CartItems,
    decimal TotalPrice);