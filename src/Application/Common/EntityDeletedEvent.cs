using Domain.Common;
using Domain.Events;
using MediatR;

namespace Application.Common;

public record EntityDeletedEvent<TEntity>(TEntity Entity) : BaseEntityDeletedEvent<TEntity>(Entity), INotification
    where TEntity : BaseEntity;