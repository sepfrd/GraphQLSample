using Application.Common;
using MediatR;

namespace Application.EntityManagement.Answers.Queries;

public record GetAllAnswersByUserExternalIdQuery(int UserExternalId, Pagination Pagination) : IRequest<QueryResponse>;