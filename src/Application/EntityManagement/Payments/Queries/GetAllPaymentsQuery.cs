using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.EntityManagement.Payments.Queries;

public record GetAllPaymentsQuery(
        Pagination Pagination,
        Expression<Func<Payment, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<Payment>>>;