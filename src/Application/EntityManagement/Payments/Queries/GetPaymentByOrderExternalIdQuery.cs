using Application.Common;
using MediatR;

namespace Application.EntityManagement.Payments.Queries;

public record GetPaymentByOrderExternalIdQuery(int OrderExternalId) : IRequest<QueryResponse>;