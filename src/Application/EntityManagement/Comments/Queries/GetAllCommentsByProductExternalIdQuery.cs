using Application.Common;
using MediatR;

namespace Application.EntityManagement.Comments.Queries;

public record GetAllCommentsByProductExternalIdQuery(int ProductExternalId, Pagination Pagination) : IRequest<QueryResponse>;