using Application.EntityManagement.Addresses.Dtos.AddressDto;
using FluentValidation;

namespace Application.EntityManagement.Shipments.Dtos.ShipmentDto;

public class ShipmentDtoValidator : AbstractValidator<ShipmentDto>
{
    public ShipmentDtoValidator()
    {
        RuleFor(shipmentDto => shipmentDto.ShipmentStatus)
            .IsInEnum()
            .WithMessage("Invalid ShipmentStatus.");

        RuleFor(shipmentDto => shipmentDto.ShippingMethod)
            .IsInEnum()
            .WithMessage("Invalid ShippingMethod.");

        RuleFor(dto => dto.DateShipped)
            .LessThanOrEqualTo(dto => dto.DateDelivered)
            .WithMessage("DateShipped should be before or equal to DateDelivered.")
            .When(dto => dto.DateShipped.HasValue && dto.DateDelivered.HasValue);

        RuleFor(dto => dto.DateDelivered)
            .GreaterThan(dto => dto.DateShipped)
            .WithMessage("DateDelivered should be after DateShipped.")
            .When(dto => dto.DateShipped.HasValue && dto.DateDelivered.HasValue);

        RuleFor(dto => dto.ShippingCost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("ShippingCost should be greater than or equal to 0.");

        RuleFor(dto => dto.DestinationAddress)
            .SetValidator(new AddressDtoValidator()!)
            .When(dto => dto.DestinationAddress is not null);

        RuleFor(dto => dto.OriginAddress)
            .SetValidator(new AddressDtoValidator()!)
            .When(dto => dto.OriginAddress is not null);
    }
}