#region

using Application.Common;
using MediatR;

#endregion

namespace Application.EntityManagement.Votes.Commands;

public record DeleteVoteByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;