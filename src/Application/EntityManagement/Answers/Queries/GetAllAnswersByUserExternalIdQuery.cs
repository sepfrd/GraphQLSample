using Application.Common;
using Application.EntityManagement.Answers.Dtos;
using MediatR;

namespace Application.EntityManagement.Answers.Queries;

public record GetAllAnswersByUserExternalIdQuery(int UserExternalId, Pagination Pagination) : IRequest<QueryReferenceResponse<IEnumerable<AnswerDto>>>;