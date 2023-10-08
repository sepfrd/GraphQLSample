using Application.Common;
using MediatR;

namespace Application.EntityManagement.Users.Queries;

public record GetUserByInternalIdQuery(Guid InternalId) : IRequest<QueryResponse>;