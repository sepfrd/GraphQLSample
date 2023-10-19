using Application.Common.Commands;
using Application.EntityManagement.Payments.Dtos;

namespace Application.EntityManagement.Payments.Commands;

public record UpdatePaymentCommand(int ExternalId, PaymentDto PaymentDto) : BaseUpdateCommand<PaymentDto>(ExternalId, PaymentDto);