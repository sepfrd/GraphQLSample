using Application.Common;
using Application.Common.Queries;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.EntityManagement.PhoneNumbers.Queries;

public record GetAllPhoneNumbersQuery(
        Pagination Pagination,
        Expression<Func<PhoneNumber, object?>>[]? RelationsToInclude = null,
        Expression<Func<PhoneNumber, bool>>? Filter = null)
    : BaseGetAllQuery<PhoneNumber>(Pagination, RelationsToInclude, Filter);