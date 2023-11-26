using Domain.Common;
using MediatR;

namespace Application.Common.Queries;

public abstract record BaseGetByInternalIdQuery<TEntity>(Guid InternalId)
    : IRequest<QueryReferenceResponse<TEntity>>
    where TEntity : BaseEntity;