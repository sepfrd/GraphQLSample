using Application.Common;
using MediatR;

namespace Application.EntityManagement.OrderItems.Commands;

public record DeleteOrderItemByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;