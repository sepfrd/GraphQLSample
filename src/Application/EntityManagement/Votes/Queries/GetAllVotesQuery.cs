using System.Linq.Expressions;
using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Votes.Queries;

public record GetAllVotesQuery(Pagination Pagination, Expression<Func<Vote, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<Vote>>>;