using Domain.Abstractions;
using Domain.Common;

namespace Domain.Events;

public abstract record BaseEntityDeletedEvent<TEntity>(TEntity Entity) : IDomainEvent
    where TEntity : BaseEntity;