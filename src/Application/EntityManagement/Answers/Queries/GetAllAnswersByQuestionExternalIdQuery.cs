using Application.Common;
using MediatR;

namespace Application.EntityManagement.Answers.Queries;

public record GetAllAnswersByQuestionExternalIdQuery(int QuestionExternalId, Pagination Pagination) : IRequest<QueryResponse>;