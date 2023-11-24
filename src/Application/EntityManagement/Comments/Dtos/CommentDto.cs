namespace Application.EntityManagement.Comments.Dtos;

public record CommentDto(
    int UserExternalId,
    int ProductExternalId,
    string Description);