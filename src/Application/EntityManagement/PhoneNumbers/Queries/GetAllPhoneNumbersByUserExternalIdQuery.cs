using Application.Common;
using MediatR;

namespace Application.EntityManagement.PhoneNumbers.Queries;

public record GetAllPhoneNumbersByUserExternalIdQuery(int UserExternalId, Pagination Pagination) : IRequest<QueryResponse>;