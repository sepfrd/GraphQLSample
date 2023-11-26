#region

using Application.Common;
using Domain.Entities;
using MediatR;

#endregion

namespace Application.EntityManagement.Users.Queries;

public sealed record GetUserByInternalIdQuery(Guid InternalId) : IRequest<QueryReferenceResponse<User>>;