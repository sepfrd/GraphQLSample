using Application.Common;
using Domain.Entities;

namespace Application.EntityManagement.Products.Events;

public record ProductDeletedEvent(Product Entity) : EntityDeletedEvent<Product>(Entity);