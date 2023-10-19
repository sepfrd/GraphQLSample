using Application.Common.Commands;
using Application.EntityManagement.Votes.Dtos;

namespace Application.EntityManagement.Votes.Commands;

public record CreateVoteCommand(VoteDto VoteDto) : BaseCreateCommand<VoteDto>(VoteDto);