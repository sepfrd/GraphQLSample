using FluentValidation;

namespace Application.EntityManagement.CartItems.Dtos.CartItemDto;

public class CartItemDtoValidator : AbstractValidator<CartItemDto>
{
    public CartItemDtoValidator()
    {
        RuleFor(cartItemDto => cartItemDto.CartExternalId)
            .GreaterThan(0)
            .WithMessage("CartExternalId should be greater than 0.");

        RuleFor(cartItemDto => cartItemDto.ProductExternalId)
            .GreaterThan(0)
            .WithMessage("ProductExternalId should be greater than 0.");

        RuleFor(cartItemDto => cartItemDto.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity should be greater than 0.");

        RuleFor(cartItemDto => cartItemDto.UnitPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("UnitPrice should be greater than or equal to 0.");
    }
}