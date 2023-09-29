using Domain.Common;
using MediatR;

namespace Application.Common.Queries;

public abstract record BaseGetByInternalIdQuery<TEntity>(Guid Id)
    : IRequest<TEntity?>
    where TEntity : BaseEntity;