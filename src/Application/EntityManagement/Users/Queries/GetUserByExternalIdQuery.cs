using Application.Common;
using Application.EntityManagement.Users.Dtos;
using MediatR;

namespace Application.EntityManagement.Users.Queries;

public sealed record GetUserByExternalIdQuery(int ExternalId) : IRequest<QueryReferenceResponse<UserDto>>;