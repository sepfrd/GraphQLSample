using Application.Common;
using MediatR;

namespace Application.EntityManagement.Products.Queries;

public record GetAllProductsByCategoryExternalIdQuery(int CategoryExternalId, Pagination Pagination) : IRequest<QueryResponse>;