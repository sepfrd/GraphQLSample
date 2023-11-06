using Application.Common;
using Application.EntityManagement.Questions.Dtos;
using MediatR;

namespace Application.EntityManagement.Questions.Queries;

public record GetAllQuestionsByUserExternalIdQuery(int UserExternalId, Pagination Pagination)
    : IRequest<QueryReferenceResponse<IEnumerable<QuestionDto>>>;