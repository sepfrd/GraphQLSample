using Application.Common;
using MediatR;

namespace Application.EntityManagement.Addresses.Commands;

public record DeleteAddressByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;