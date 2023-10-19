using Application.Common;
using MediatR;

namespace Application.EntityManagement.Questions.Queries;

public record GetAllQuestionsByUserExternalIdQuery(int UserExternalId, Pagination Pagination) : IRequest<QueryResponse>;