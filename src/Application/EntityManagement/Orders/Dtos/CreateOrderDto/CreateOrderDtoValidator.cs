using Application.EntityManagement.OrderItems.Dtos.CreateOrderItemDto;
using Application.EntityManagement.Payments.Dtos.PaymentDto;
using Application.EntityManagement.Shipments.Dtos.CreateShipmentDto;
using FluentValidation;

namespace Application.EntityManagement.Orders.Dtos.CreateOrderDto;

public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderDtoValidator()
    {
        RuleFor(createOrderDto => createOrderDto.PaymentDto)
            .SetValidator(new PaymentDtoValidator());

        RuleFor(createOrderDto => createOrderDto.CreateShipmentDto)
            .SetValidator(new CreateShipmentDtoValidator());

        RuleForEach(createOrderDto => createOrderDto.CreateOrderItemDtos)
            .SetValidator(new CreateOrderItemDtoValidator());
    }
}