using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Orders.Handlers;

public class GetAllOrdersQueryHandler : BaseGetAllQueryHandler<Order>
{
    public GetAllOrdersQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}