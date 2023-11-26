using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.EntityManagement.Answers.Queries;

public record GetAllAnswersQuery(
        Pagination Pagination,
        Expression<Func<Answer, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<Answer>>>;