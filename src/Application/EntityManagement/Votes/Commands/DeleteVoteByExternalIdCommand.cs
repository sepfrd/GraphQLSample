using Application.Common;
using MediatR;

namespace Application.EntityManagement.Votes.Commands;

public record DeleteVoteByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;