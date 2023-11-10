using Application.Common;
using Application.Common.Queries;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.EntityManagement.Persons.Queries;

public record GetAllPersonsQuery(
        Pagination Pagination,
        Expression<Func<Person, object?>>[]? RelationsToInclude = null,
        Expression<Func<Person, bool>>? Filter = null)
    : BaseGetAllQuery<Person>(Pagination, RelationsToInclude, Filter);