using Application.Common;
using Application.EntityManagement.PhoneNumbers.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.PhoneNumbers.Handlers;

public class GetAllPhoneNumbersQueryHandler(IRepository<PhoneNumber> repository)
    : IRequestHandler<GetAllPhoneNumbersQuery, QueryReferenceResponse<IEnumerable<PhoneNumber>>>
{
    public virtual async Task<QueryReferenceResponse<IEnumerable<PhoneNumber>>> Handle(GetAllPhoneNumbersQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, cancellationToken, request.RelationsToInclude);

        return new QueryReferenceResponse<IEnumerable<PhoneNumber>>
            (
            entities.Paginate(request.Pagination),
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}