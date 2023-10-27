using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Payments.Dtos;
using Application.EntityManagement.Payments.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.EntityManagement.Payments.Handlers;

public class GetPaymentsByUserExternalIdQueryHandler : IRequestHandler<GetPaymentsByUserExternalIdQuery, QueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public GetPaymentsByUserExternalIdQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<QueryResponse> Handle(GetPaymentsByUserExternalIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork
            .UserRepository
            .GetByExternalIdAsync(request.UserExternalId, cancellationToken,
            entity => entity.Payments);

        if (user is null)
        {
            return new QueryResponse
                (
                null,
                false,
                Messages.NotFound,
                HttpStatusCode.NotFound
                );
        }

        if (user.Payments is null)
        {
            _logger.LogError(Messages.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(User), typeof(GetPaymentsByUserExternalIdQueryHandler));

            return new QueryResponse
                (
                null,
                false,
                Messages.InternalServerError,
                HttpStatusCode.InternalServerError
                );
        }

        var paymentDtos = _mappingService.Map<ICollection<Payment>, ICollection<PaymentDto>>(user.Payments);

        if (paymentDtos is not null)
        {
            return new QueryResponse
                (
                paymentDtos.Paginate(request.Pagination),
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK
                );
        }

        _logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(ICollection<Payment>), typeof(GetPaymentsByUserExternalIdQueryHandler));

        return new QueryResponse
            (
            null,
            false,
            Messages.InternalServerError,
            HttpStatusCode.InternalServerError
            );
    }
}