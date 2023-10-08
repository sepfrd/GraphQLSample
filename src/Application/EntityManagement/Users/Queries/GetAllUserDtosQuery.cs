using Application.Common;
using MediatR;

namespace Application.EntityManagement.Users.Queries;

public record GetAllUserDtosQuery : IRequest<QueryResponse>;