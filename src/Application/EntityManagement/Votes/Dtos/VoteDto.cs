using Domain.Enums;

namespace Application.EntityManagement.Votes.Dtos;

public record VoteDto(VoteType Type, VotableContentType ContentType, int ContentExternalId);