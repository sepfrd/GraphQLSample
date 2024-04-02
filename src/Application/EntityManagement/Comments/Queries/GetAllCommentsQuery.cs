using System.Linq.Expressions;
using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Comments.Queries;

public record GetAllCommentsQuery(
        Pagination Pagination,
        Expression<Func<Comment, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<Comment>>>;