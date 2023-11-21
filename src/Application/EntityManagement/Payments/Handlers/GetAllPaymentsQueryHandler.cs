using System.Net;
using Application.Common;
using Application.EntityManagement.Payments.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Payments.Handlers;

public class GetAllPaymentsQueryHandler(IRepository<Payment> repository)
    : IRequestHandler<GetAllPaymentsQuery, QueryReferenceResponse<IEnumerable<Payment>>>
{
    public virtual async Task<QueryReferenceResponse<IEnumerable<Payment>>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Payment>>
        (
            entities.Paginate(request.Pagination),
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
        );
    }
}