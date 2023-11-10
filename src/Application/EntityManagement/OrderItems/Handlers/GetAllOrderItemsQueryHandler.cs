using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.OrderItems.Handlers;

public class GetAllOrderItemsQueryHandler : BaseGetAllQueryHandler<OrderItem>
{
    public GetAllOrderItemsQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}