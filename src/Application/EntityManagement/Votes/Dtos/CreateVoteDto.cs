using Domain.Enums;

namespace Application.EntityManagement.Votes.Dtos;

public record CreateVoteDto(VoteType Type, VotableContentType ContentType, int UserExternalId, int ContentExternalId);