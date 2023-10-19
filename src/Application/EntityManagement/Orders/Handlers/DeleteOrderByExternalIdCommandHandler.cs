using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Orders.Handlers;

public class DeleteOrderByExternalIdCommandHandler : BaseDeleteByExternalIdCommandHandler<Order>
{
    public DeleteOrderByExternalIdCommandHandler(IUnitOfWork unitOfWork, ILogger logger) : base(unitOfWork, logger)
    {
    }
}