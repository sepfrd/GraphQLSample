#region

using Application.Common;
using Application.EntityManagement.Users.Dtos;
using MediatR;

#endregion

namespace Application.EntityManagement.Users.Queries;

public sealed record GetUserByExternalIdQuery(int ExternalId) : IRequest<QueryReferenceResponse<UserDto>>;