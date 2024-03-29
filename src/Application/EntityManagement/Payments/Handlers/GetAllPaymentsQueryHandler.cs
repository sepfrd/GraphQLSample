using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Payments.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Payments.Handlers;

public sealed class GetAllPaymentsQueryHandler : IRequestHandler<GetAllPaymentsQuery, QueryResponse<IEnumerable<Payment>>>
{
    private readonly IRepository<Payment> _repository;

    public GetAllPaymentsQueryHandler(IRepository<Payment> repository)
    {
        _repository = repository;
    }

    public async Task<QueryResponse<IEnumerable<Payment>>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryResponse<IEnumerable<Payment>>(
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}