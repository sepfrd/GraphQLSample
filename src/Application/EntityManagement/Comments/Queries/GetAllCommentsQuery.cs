#region

using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

#endregion

namespace Application.EntityManagement.Comments.Queries;

public record GetAllCommentsQuery(
        Pagination Pagination,
        Expression<Func<Comment, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<Comment>>>;