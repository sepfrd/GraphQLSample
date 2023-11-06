using Application.Common;
using Application.EntityManagement.Payments.Dtos;
using MediatR;

namespace Application.EntityManagement.Payments.Queries;

public record GetPaymentsByUserExternalIdQuery(int UserExternalId, Pagination Pagination) : IRequest<QueryReferenceResponse<IEnumerable<PaymentDto>>>;