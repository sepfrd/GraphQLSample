using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.OrderItems.Handlers;

public class DeleteOrderItemByExternalIdCommandHandler(
        IRepository<OrderItem> orderItemRepository,
        ILogger logger)
    : BaseDeleteByExternalIdCommandHandler<OrderItem>(orderItemRepository, logger);