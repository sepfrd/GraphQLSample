using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.EntityManagement.Addresses.Queries;

public record GetAllAddressesQuery(
        Pagination Pagination,
        Expression<Func<Address, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<Address>>>;