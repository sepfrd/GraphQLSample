using Application.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.EntityManagement.Votes.Queries;

public record GetAllVotesQuery(
        Pagination Pagination,
        Expression<Func<Vote, object?>>[]? RelationsToInclude = null,
        Expression<Func<Vote, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<Vote>>>;