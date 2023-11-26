using Domain.Abstractions;
using Domain.Enums;

namespace Application.EntityManagement.Votes.Dtos;

public record VoteDto(VoteType Type, IVotableContent Content);