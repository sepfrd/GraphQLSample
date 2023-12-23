using FluentValidation;

namespace Application.EntityManagement.Payments.Dtos.PaymentDto;

public class PaymentDtoValidator : AbstractValidator<PaymentDto>
{
    public PaymentDtoValidator()
    {
        RuleFor(paymentDto => paymentDto.OrderExternalId)
            .GreaterThan(0)
            .WithMessage("OrderExternalId should be greater than 0.");

        RuleFor(paymentDto => paymentDto.Amount)
            .GreaterThan(0)
            .WithMessage("Amount should be greater than 0.");

        RuleFor(paymentDto => paymentDto.PaymentMethod)
            .IsInEnum()
            .WithMessage("Invalid PaymentMethod.");

        RuleFor(paymentDto => paymentDto.PaymentStatus)
            .IsInEnum()
            .WithMessage("Invalid PaymentStatus.");
    }
}