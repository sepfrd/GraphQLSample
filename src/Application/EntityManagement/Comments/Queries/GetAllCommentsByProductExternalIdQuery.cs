using Application.Common;
using Application.EntityManagement.Comments.Dtos;
using MediatR;

namespace Application.EntityManagement.Comments.Queries;

public record GetAllCommentsByProductExternalIdQuery(int ProductExternalId, Pagination Pagination) : IRequest<QueryReferenceResponse<IEnumerable<CommentDto>>>;