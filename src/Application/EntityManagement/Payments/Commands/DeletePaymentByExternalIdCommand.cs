#region

using Application.Common;
using MediatR;

#endregion

namespace Application.EntityManagement.Payments.Commands;

public record DeletePaymentByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;