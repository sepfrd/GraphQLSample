using Application.Common;
using MediatR;

namespace Application.EntityManagement.Users.Queries;

public record GetUserByUsernameOrEmailQuery(string UsernameOrEmail) : IRequest<QueryResponse>;