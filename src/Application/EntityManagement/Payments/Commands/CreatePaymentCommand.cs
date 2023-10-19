using Application.Common.Commands;
using Application.EntityManagement.Payments.Dtos;

namespace Application.EntityManagement.Payments.Commands;

public record CreatePaymentCommand(PaymentDto PaymentDto) : BaseCreateCommand<PaymentDto>(PaymentDto);