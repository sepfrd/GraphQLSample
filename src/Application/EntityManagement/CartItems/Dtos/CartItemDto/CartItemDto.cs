namespace Application.EntityManagement.CartItems.Dtos.CartItemDto;

public record CartItemDto(
    int CartExternalId,
    int ProductExternalId,
    int Quantity,
    decimal UnitPrice);