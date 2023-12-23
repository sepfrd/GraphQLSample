using FluentValidation;

namespace Application.EntityManagement.OrderItems.Dtos.CreateOrderItemDto;

public class CreateOrderItemDtoValidator : AbstractValidator<CreateOrderItemDto>
{
    public CreateOrderItemDtoValidator()
    {
        RuleFor(createOrderItemDto => createOrderItemDto.ProductExternalId)
            .GreaterThan(0)
            .WithMessage("ProductExternalId should be greater than 0.");

        RuleFor(createOrderItemDto => createOrderItemDto.UnitPrice)
            .GreaterThan(0)
            .WithMessage("UnitPrice should be greater than 0.");

        RuleFor(createOrderItemDto => createOrderItemDto.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity should be greater than 0.");
    }
}