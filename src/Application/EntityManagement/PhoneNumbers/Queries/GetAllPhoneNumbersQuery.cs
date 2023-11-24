using System.Linq.Expressions;
using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.PhoneNumbers.Queries;

public record GetAllPhoneNumbersQuery(
        Pagination Pagination,
        Expression<Func<PhoneNumber, object?>>[]? RelationsToInclude = null,
        Expression<Func<PhoneNumber, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<PhoneNumber>>>;