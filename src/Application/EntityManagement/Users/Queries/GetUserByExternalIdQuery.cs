using Application.Common;
using Application.EntityManagement.Users.Dtos;
using Application.EntityManagement.Users.Dtos.UserDto;
using MediatR;

namespace Application.EntityManagement.Users.Queries;

public sealed record GetUserByExternalIdQuery(int ExternalId) : IRequest<QueryResponse<UserDto>>;