using System.Linq.Expressions;
using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Comments.Queries;

public record GetAllCommentsQuery(Expression<Func<Comment, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<Comment>>>;