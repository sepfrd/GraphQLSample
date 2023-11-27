using Application.Common;
using Domain.Entities;

namespace Application.EntityManagement.Orders.Events;

public record OrderDeletedEvent(Order Entity) : EntityDeletedEvent<Order>(Entity);