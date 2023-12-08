using Application.Common;
using Application.EntityManagement.Payments.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Payments.Handlers;

public sealed class GetAllPaymentsQueryHandler(IRepository<Payment> repository)
    : IRequestHandler<GetAllPaymentsQuery, QueryReferenceResponse<IEnumerable<Payment>>>
{
    public async Task<QueryReferenceResponse<IEnumerable<Payment>>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Payment>>(
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}