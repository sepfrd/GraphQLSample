#region

using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

#endregion

namespace Application.EntityManagement.PhoneNumbers.Queries;

public record GetAllPhoneNumbersQuery(
        Pagination Pagination,
        Expression<Func<PhoneNumber, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<PhoneNumber>>>;