using Domain.Common;
using MediatR;

namespace Application.Common.Queries;

public abstract record BaseGetAllDtosQuery<TEntity, TDto>()
    : IRequest<IEnumerable<TDto>?>
    where TEntity : BaseEntity;