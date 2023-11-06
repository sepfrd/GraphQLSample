using Application.Common;
using Application.EntityManagement.Products.Dtos;
using MediatR;

namespace Application.EntityManagement.Products.Queries;

public record GetAllProductsByCategoryExternalIdQuery(int CategoryExternalId, Pagination Pagination)
    : IRequest<QueryReferenceResponse<IEnumerable<ProductDto>>>;