using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.PhoneNumbers.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.PhoneNumbers.Handlers;

public sealed class GetAllPhoneNumbersQueryHandler : IRequestHandler<GetAllPhoneNumbersQuery, QueryResponse<IEnumerable<PhoneNumber>>>
{
    private readonly IRepository<PhoneNumber> _repository;

    public GetAllPhoneNumbersQueryHandler(IRepository<PhoneNumber> repository)
    {
        _repository = repository;
    }

    public async Task<QueryResponse<IEnumerable<PhoneNumber>>> Handle(GetAllPhoneNumbersQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryResponse<IEnumerable<PhoneNumber>>(
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}