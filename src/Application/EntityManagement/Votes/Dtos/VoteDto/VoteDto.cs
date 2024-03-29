using Domain.Enums;

namespace Application.EntityManagement.Votes.Dtos.VoteDto;

public record VoteDto(VoteType Type, VotableContentType ContentType, int ContentExternalId);