using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Orders.Handlers;

public class GetAllOrdersQueryHandler(IRepository<Order> orderRepository)
    : BaseGetAllQueryHandler<Order>(orderRepository);