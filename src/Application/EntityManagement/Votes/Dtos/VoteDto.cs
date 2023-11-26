#region

using Domain.Abstractions;
using Domain.Enums;

#endregion

namespace Application.EntityManagement.Votes.Dtos;

public record VoteDto(VoteType Type, IVotableContent Content);