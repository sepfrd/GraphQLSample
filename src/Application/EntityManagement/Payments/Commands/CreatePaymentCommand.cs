using Application.Common;
using Application.EntityManagement.Payments.Dtos;
using Application.EntityManagement.Payments.Dtos.PaymentDto;
using MediatR;

namespace Application.EntityManagement.Payments.Commands;

public record CreatePaymentCommand(PaymentDto PaymentDto) : IRequest<CommandResult>;