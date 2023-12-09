using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Orders.Commands;
using Application.EntityManagement.Orders.Events;
using Application.EntityManagement.Orders.Queries;
using Domain.Common;
using MediatR;

namespace Application.EntityManagement.Orders;

public class OrderService
{
    private readonly IMediator _mediator;

    public OrderService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<CommandResult> DeleteByExternalIdAsync(int externalId, CancellationToken cancellationToken = default)
    {
        var pagination = new Pagination();

        var ordersQuery = new GetAllOrdersQuery(pagination, order => order.ExternalId == externalId);

        var orderResult = await _mediator.Send(ordersQuery, cancellationToken);

        if (!orderResult.IsSuccessful ||
            orderResult.Data is null ||
            !orderResult.Data.Any())
        {
            return CommandResult.Failure(MessageConstants.NotFound);
        }

        var deleteOrderCommand = new DeleteOrderByExternalIdCommand(externalId);

        await _mediator.Send(deleteOrderCommand, cancellationToken);

        var orderDeletedEvent = new OrderDeletedEvent(orderResult.Data.First());

        await _mediator.Publish(orderDeletedEvent, cancellationToken);

        return CommandResult.Success(MessageConstants.SuccessfullyDeleted);
    }
}