using Domain.Common;
using MediatR;

namespace Application.Common.Queries;

public abstract record BaseGetAllQuery<TEntity>
    : IRequest<IEnumerable<TEntity>>
    where TEntity : BaseEntity;