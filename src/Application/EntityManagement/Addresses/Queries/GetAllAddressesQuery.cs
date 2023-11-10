using Application.Common;
using Application.Common.Queries;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.EntityManagement.Addresses.Queries;

public record GetAllAddressesQuery(
        Pagination Pagination,
        Expression<Func<Address, object?>>[]? RelationsToInclude = null,
        Expression<Func<Address, bool>>? Filter = null)
    : BaseGetAllQuery<Address>(Pagination, RelationsToInclude, Filter);