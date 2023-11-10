using Application.Common;
using Application.Common.Queries;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.EntityManagement.Categories.Queries;

public record GetAllCategoriesQuery(
        Pagination Pagination,
        Expression<Func<Category, object?>>[]? RelationsToInclude = null,
        Expression<Func<Category, bool>>? Filter = null)
    : BaseGetAllQuery<Category>(Pagination, RelationsToInclude, Filter);