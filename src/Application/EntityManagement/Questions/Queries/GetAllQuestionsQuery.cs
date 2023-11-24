using System.Linq.Expressions;
using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Questions.Queries;

public record GetAllQuestionsQuery(
        Pagination Pagination,
        Expression<Func<Question, object?>>[]? RelationsToInclude = null,
        Expression<Func<Question, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<Question>>>;