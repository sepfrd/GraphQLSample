namespace Application.EntityManagement.CartItems.Dtos;

public record CreateCartItemDto(
    int CartExternalId,
    int ProductExternalId,
    int Quantity,
    decimal UnitPrice,
    decimal SubTotalPrice);