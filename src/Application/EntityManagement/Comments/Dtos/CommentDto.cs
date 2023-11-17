using Application.EntityManagement.Votes.Dtos;

namespace Application.EntityManagement.Comments.Dtos;

public record CommentDto(
    int ExternalId,
    int UserExternalId,
    int ProductExternalId,
    string Description);