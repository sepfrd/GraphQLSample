using Application.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.EntityManagement.Comments.Queries;

public record GetAllCommentsQuery(
        Pagination Pagination,
        Expression<Func<Comment, object?>>[]? RelationsToInclude = null,
        Expression<Func<Comment, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<Comment>>>;