using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.OrderItems.Handlers;

public class GetAllOrderItemsQueryHandler(IRepository<OrderItem> orderItemRepository)
    : BaseGetAllQueryHandler<OrderItem>(orderItemRepository);