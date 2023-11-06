using Application.Common;
using Application.EntityManagement.Payments.Dtos;
using MediatR;

namespace Application.EntityManagement.Payments.Queries;

public record GetPaymentByOrderExternalIdQuery(int OrderExternalId) : IRequest<QueryReferenceResponse<PaymentDto>>;