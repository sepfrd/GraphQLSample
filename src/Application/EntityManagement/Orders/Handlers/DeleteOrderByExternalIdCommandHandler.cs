using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Orders.Handlers;

public class DeleteOrderByExternalIdCommandHandler(IRepository<Order> orderRepository, ILogger logger)
    : BaseDeleteByExternalIdCommandHandler<Order>(orderRepository, logger);