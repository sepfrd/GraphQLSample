using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Orders.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Orders.Handlers;

public class CreateOrderCommandHandler(
        IRepository<Order> orderRepository,
        IMappingService mappingService,
        ILogger logger)
    : BaseCreateCommandHandler<Order, OrderDto>(orderRepository, mappingService, logger);