using Application.Common;
using Application.EntityManagement.Answers.Dtos;
using MediatR;

namespace Application.EntityManagement.Answers.Queries;

public record GetAllAnswersByQuestionExternalIdQuery(int QuestionExternalId, Pagination Pagination) : IRequest<QueryReferenceResponse<IEnumerable<AnswerDto>>>;