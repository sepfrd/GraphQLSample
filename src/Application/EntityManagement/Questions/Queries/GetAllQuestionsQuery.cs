#region

using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

#endregion

namespace Application.EntityManagement.Questions.Queries;

public record GetAllQuestionsQuery(
        Pagination Pagination,
        Expression<Func<Question, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<Question>>>;