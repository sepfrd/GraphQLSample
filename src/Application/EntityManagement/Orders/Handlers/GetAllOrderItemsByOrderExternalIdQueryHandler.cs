using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.OrderItems.Dtos;
using Application.EntityManagement.Orders.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.EntityManagement.Orders.Handlers;

public class GetAllOrderItemsByOrderExternalIdQueryHandler : IRequestHandler<GetAllOrderItemsByOrderExternalIdQuery, QueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public GetAllOrderItemsByOrderExternalIdQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<QueryResponse> Handle(GetAllOrderItemsByOrderExternalIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.OrderRepository.GetByExternalIdAsync(request.OrderExternalId, new Func<Order, object?>[]
            {
                entity => entity.OrderItems
            },
            cancellationToken);

        if (order is null)
        {
            return new QueryResponse
                (
                null,
                true,
                Messages.NotFound,
                HttpStatusCode.NotFound
                );
        }

        if (order.OrderItems is null)
        {
            _logger.LogError(Messages.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(Order), typeof(GetAllOrderItemsByOrderExternalIdQueryHandler));

            return new QueryResponse
                (
                null,
                false,
                Messages.InternalServerError,
                HttpStatusCode.InternalServerError
                );
        }

        var orderItemDtos = _mappingService.Map<ICollection<OrderItem>, ICollection<OrderItemDto>>(order.OrderItems);

        if (orderItemDtos is not null)
        {
            return new QueryResponse
                (
                orderItemDtos,
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK
                );
        }

        _logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(ICollection<OrderItem>), typeof(GetAllOrderItemsByOrderExternalIdQueryHandler));

        return new QueryResponse
            (
            null,
            false,
            Messages.InternalServerError,
            HttpStatusCode.InternalServerError
            );
    }
}