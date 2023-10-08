using Application.Common;
using MediatR;

namespace Application.EntityManagement.Users.Queries;

public record GetAllUsersQuery : IRequest<QueryResponse>;