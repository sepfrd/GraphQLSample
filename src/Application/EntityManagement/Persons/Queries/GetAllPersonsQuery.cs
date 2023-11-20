using Application.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.EntityManagement.Persons.Queries;

public record GetAllPersonsQuery(
        Pagination Pagination,
        Expression<Func<Person, object?>>[]? RelationsToInclude = null,
        Expression<Func<Person, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<Person>>>;