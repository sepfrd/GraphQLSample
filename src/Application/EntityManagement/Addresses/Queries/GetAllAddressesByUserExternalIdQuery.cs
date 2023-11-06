using Application.Common;
using Application.EntityManagement.Addresses.Dtos;
using MediatR;

namespace Application.EntityManagement.Addresses.Queries;

public record GetAllAddressesByUserExternalIdQuery(int UserExternalId, Pagination Pagination) : IRequest<QueryReferenceResponse<IEnumerable<AddressDto>>>;