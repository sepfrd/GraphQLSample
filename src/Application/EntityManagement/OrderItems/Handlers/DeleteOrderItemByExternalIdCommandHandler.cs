using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.OrderItems.Handlers;

public class DeleteOrderItemByExternalIdCommandHandler : BaseDeleteByExternalIdCommandHandler<OrderItem>
{
    public DeleteOrderItemByExternalIdCommandHandler(IUnitOfWork unitOfWork, ILogger logger) : base(unitOfWork, logger)
    {
    }
}