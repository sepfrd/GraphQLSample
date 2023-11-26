#region

using Application.Common;
using MediatR;

#endregion

namespace Application.EntityManagement.Addresses.Commands;

public record DeleteAddressByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;