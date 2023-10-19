using Application.Common;
using MediatR;

namespace Application.EntityManagement.Payments.Queries;

public record GetPaymentsByUserExternalIdQuery(int UserExternalId, Pagination Pagination) : IRequest<QueryResponse>;