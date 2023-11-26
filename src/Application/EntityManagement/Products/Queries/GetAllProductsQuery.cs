using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.EntityManagement.Products.Queries;

public record GetAllProductsQuery(
        Pagination Pagination,
        Expression<Func<Product, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<Product>>>;