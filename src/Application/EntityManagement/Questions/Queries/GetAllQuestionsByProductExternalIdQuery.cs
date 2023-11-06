using Application.Common;
using Application.EntityManagement.Questions.Dtos;
using MediatR;

namespace Application.EntityManagement.Questions.Queries;

public record GetAllQuestionsByProductExternalIdQuery(int ProductExternalId, Pagination Pagination)
    : IRequest<QueryReferenceResponse<IEnumerable<QuestionDto>>>;