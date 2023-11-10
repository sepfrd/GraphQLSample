using Application.Common;
using Application.Common.Queries;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.EntityManagement.Products.Queries;

public record GetAllProductsQuery(
        Pagination Pagination,
        Expression<Func<Product, object?>>[]? RelationsToInclude = null,
        Expression<Func<Product, bool>>? Filter = null)
    : BaseGetAllQuery<Product>(Pagination, RelationsToInclude, Filter);