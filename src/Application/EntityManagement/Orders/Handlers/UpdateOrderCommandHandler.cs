using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Orders.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Orders.Handlers;

public class UpdateOrderCommandHandler : BaseUpdateCommandHandler<Order, OrderDto>
{
    public UpdateOrderCommandHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger) : base(unitOfWork, mappingService, logger)
    {
    }
}