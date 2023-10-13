using Application.EntityManagement.CartItems.Dtos;
using Application.EntityManagement.Users.Dtos;

namespace Application.EntityManagement.Carts.Dtos;

public record CartDto(int ExternalId, int UserExternalId, ICollection<CartItemDto>? CartItemDtos, decimal TotalPrice);