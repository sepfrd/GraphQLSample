using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.OrderItems.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.OrderItems.Handlers;

public class CreateOrderItemCommandHandler(
        IRepository<OrderItem> repository,
        IMappingService mappingService,
        ILogger logger)
    : BaseCreateCommandHandler<OrderItem, OrderItemDto>(repository, mappingService, logger);