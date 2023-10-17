using Application.Common;
using MediatR;

namespace Application.EntityManagement.Products.Queries;

public record GetCommentsByProductExternalIdQuery(int ProductExternalId) : IRequest<QueryResponse>;