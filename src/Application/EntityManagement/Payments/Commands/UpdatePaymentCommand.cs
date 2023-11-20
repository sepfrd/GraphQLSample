using Application.Common;
using Application.EntityManagement.Payments.Dtos;
using MediatR;

namespace Application.EntityManagement.Payments.Commands;

public record UpdatePaymentCommand(int ExternalId, PaymentDto PaymentDto) : IRequest<CommandResult>;