using Application.Common;
using MediatR;

namespace Application.EntityManagement.Payments.Commands;

public record DeletePaymentByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;