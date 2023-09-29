using Domain.Common;
using MediatR;

namespace Application.Common.Queries;

public abstract record BaseGetByExternalIdQuery<TEntity, TDto>(int Id)
    : IRequest<TDto?>
    where TEntity : BaseEntity;