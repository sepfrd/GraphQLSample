using System.Linq.Expressions;
using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Payments.Queries;

public record GetAllPaymentsQuery(Expression<Func<Payment, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<Payment>>>;