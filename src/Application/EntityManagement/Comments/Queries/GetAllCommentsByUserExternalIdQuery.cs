using Application.Common;
using MediatR;

namespace Application.EntityManagement.Comments.Queries;

public record GetAllCommentsByUserExternalIdQuery(int UserExternalId, Pagination Pagination) : IRequest<QueryResponse>;