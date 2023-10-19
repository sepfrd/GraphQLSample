using Application.Common;
using MediatR;

namespace Application.EntityManagement.Questions.Queries;

public record GetAllQuestionsByProductExternalIdQuery(int ProductExternalId, Pagination Pagination) : IRequest<QueryResponse>;