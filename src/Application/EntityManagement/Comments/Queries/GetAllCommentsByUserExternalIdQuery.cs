using Application.Common;
using Application.EntityManagement.Comments.Dtos;
using MediatR;

namespace Application.EntityManagement.Comments.Queries;

public record GetAllCommentsByUserExternalIdQuery(int UserExternalId, Pagination Pagination) : IRequest<QueryReferenceResponse<IEnumerable<CommentDto>>>;