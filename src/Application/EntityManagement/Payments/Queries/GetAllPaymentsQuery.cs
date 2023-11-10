using Application.Common;
using Application.Common.Queries;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.EntityManagement.Payments.Queries;

public record GetAllPaymentsQuery(
        Pagination Pagination,
        Expression<Func<Payment, object?>>[]? RelationsToInclude = null,
        Expression<Func<Payment, bool>>? Filter = null)
    : BaseGetAllQuery<Payment>(Pagination, RelationsToInclude, Filter);