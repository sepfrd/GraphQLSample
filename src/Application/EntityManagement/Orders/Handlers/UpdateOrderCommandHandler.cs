using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Orders.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Orders.Handlers;

public class UpdateOrderCommandHandler(
        IRepository<Order> orderRepository,
        IMappingService mappingService,
        ILogger logger)
    : BaseUpdateCommandHandler<Order, OrderDto>(orderRepository, mappingService, logger);