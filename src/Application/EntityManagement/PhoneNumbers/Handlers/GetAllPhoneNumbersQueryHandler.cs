using Application.Common;
using Application.EntityManagement.PhoneNumbers.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.PhoneNumbers.Handlers;

public sealed class GetAllPhoneNumbersQueryHandler(IRepository<PhoneNumber> repository)
    : IRequestHandler<GetAllPhoneNumbersQuery, QueryReferenceResponse<IEnumerable<PhoneNumber>>>
{
    public async Task<QueryReferenceResponse<IEnumerable<PhoneNumber>>> Handle(GetAllPhoneNumbersQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<PhoneNumber>>(
            entities,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}