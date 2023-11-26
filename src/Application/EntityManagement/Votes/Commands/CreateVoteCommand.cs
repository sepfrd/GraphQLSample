#region

using Application.Common;
using Application.EntityManagement.Votes.Dtos;
using MediatR;

#endregion

namespace Application.EntityManagement.Votes.Commands;

public record CreateVoteCommand(VoteDto VoteDto) : IRequest<CommandResult>;