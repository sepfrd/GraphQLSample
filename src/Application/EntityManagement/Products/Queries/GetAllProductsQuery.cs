using System.Linq.Expressions;
using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Products.Queries;

public record GetAllProductsQuery(
        Pagination Pagination,
        Expression<Func<Product, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<Product>>>;