using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.OrderItems.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.OrderItems.Handlers;

public class UpdateOrderItemCommandHandler(
        IRepository<OrderItem> orderItemRepository,
        IMappingService mappingService,
        ILogger logger)
    : BaseUpdateCommandHandler<OrderItem, OrderItemDto>(orderItemRepository, mappingService, logger);