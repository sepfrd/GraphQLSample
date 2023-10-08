using Application.Common;
using MediatR;

namespace Application.EntityManagement.Users.Queries;

public record GetUserByExternalIdQuery(int ExternalId) : IRequest<QueryResponse>;