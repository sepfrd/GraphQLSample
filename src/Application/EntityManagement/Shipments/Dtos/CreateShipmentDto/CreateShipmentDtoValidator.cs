using FluentValidation;

namespace Application.EntityManagement.Shipments.Dtos.CreateShipmentDto;

public class CreateShipmentDtoValidator : AbstractValidator<CreateShipmentDto>
{
    public CreateShipmentDtoValidator()
    {
        RuleFor(createShipmentDto => createShipmentDto.OriginAddressExternalId)
            .GreaterThan(0)
            .WithMessage("OriginAddressExternalId should be greater than 0.");

        RuleFor(createShipmentDto => createShipmentDto.DestinationAddressExternalId)
            .GreaterThan(0)
            .WithMessage("DestinationAddressExternalId should be greater than 0.");

        RuleFor(createShipmentDto => createShipmentDto.ShippingMethod)
            .IsInEnum()
            .WithMessage("Invalid ShippingMethod.");

        RuleFor(createShipmentDto => createShipmentDto.DateToBeShipped)
            .GreaterThan(DateTime.Now)
            .WithMessage("DateToBeShipped should be in the future.");

        RuleFor(createShipmentDto => createShipmentDto.DateToBeDelivered)
            .GreaterThan(dto => dto.DateToBeShipped)
            .WithMessage("DateToBeDelivered should be after DateToBeShipped.");

        RuleFor(createShipmentDto => createShipmentDto.ShippingCost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("ShippingCost should be greater than or equal to 0.");
    }
}