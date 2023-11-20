using Application.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.EntityManagement.Addresses.Queries;

public record GetAllAddressesQuery(
        Pagination Pagination,
        Expression<Func<Address, object?>>[]? RelationsToInclude = null,
        Expression<Func<Address, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<Address>>>;