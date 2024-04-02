using System.Linq.Expressions;
using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Questions.Queries;

public record GetAllQuestionsQuery(
        Pagination Pagination,
        Expression<Func<Question, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<Question>>>;