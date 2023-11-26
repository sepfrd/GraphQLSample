using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Users.Queries;

public record GetUserByUsernameOrEmailQuery(string UsernameOrEmail) : IRequest<QueryReferenceResponse<User>>;