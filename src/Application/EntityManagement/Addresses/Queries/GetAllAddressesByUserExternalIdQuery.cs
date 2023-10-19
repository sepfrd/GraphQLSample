using Application.Common;
using MediatR;

namespace Application.EntityManagement.Addresses.Queries;

public record GetAllAddressesByUserExternalIdQuery(int UserExternalId, Pagination Pagination) : IRequest<QueryResponse>;