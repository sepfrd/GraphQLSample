using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Orders.Dtos;
using Application.EntityManagement.Orders.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.EntityManagement.Orders.Handlers;

public class GetOrdersByUserExternalIdQueryHandler : IRequestHandler<GetOrdersByUserExternalIdQuery, QueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public GetOrdersByUserExternalIdQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<QueryResponse> Handle(GetOrdersByUserExternalIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByExternalIdAsync(request.UserExternalId,
            new Func<User, object?>[]
            {
                entity => entity.Orders
            },
            cancellationToken);

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

        if (user.Orders is null)
        {
            _logger.LogError(Messages.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(User), typeof(GetOrdersByUserExternalIdQueryHandler));

            return new QueryResponse
                (
                null,
                false,
                Messages.InternalServerError,
                HttpStatusCode.InternalServerError
                );
        }

        var orderDtos = _mappingService.Map<ICollection<Order>, ICollection<OrderDto>>(user.Orders);

        if (orderDtos is not null)
        {
            return new QueryResponse
                (
                orderDtos,
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK
                );
        }

        _logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(ICollection<Order>), typeof(GetOrdersByUserExternalIdQueryHandler));

        return new QueryResponse
            (
            null,
            false,
            Messages.InternalServerError,
            HttpStatusCode.InternalServerError
            );
    }
}