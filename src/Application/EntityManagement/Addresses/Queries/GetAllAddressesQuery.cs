using System.Linq.Expressions;
using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Addresses.Queries;

public record GetAllAddressesQuery(Expression<Func<Address, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<Address>>>;