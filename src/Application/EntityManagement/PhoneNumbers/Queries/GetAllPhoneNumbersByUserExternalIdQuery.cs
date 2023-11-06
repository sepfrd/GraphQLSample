using Application.Common;
using Application.EntityManagement.PhoneNumbers.Dtos;
using MediatR;

namespace Application.EntityManagement.PhoneNumbers.Queries;

public record GetAllPhoneNumbersByUserExternalIdQuery(int UserExternalId, Pagination Pagination) : IRequest<QueryReferenceResponse<IEnumerable<PhoneNumberDto>>>;