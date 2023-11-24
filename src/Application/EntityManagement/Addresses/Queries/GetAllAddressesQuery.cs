using System.Linq.Expressions;
using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Addresses.Queries;

public record GetAllAddressesQuery(
        Pagination Pagination,
        Expression<Func<Address, object?>>[]? RelationsToInclude = null,
        Expression<Func<Address, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<Address>>>;