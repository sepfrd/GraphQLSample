using Domain.Common;
using MediatR;

namespace Application.Common.Queries;

public abstract record BaseGetByExternalIdQuery<TEntity>(int ExternalId)
    : IRequest<QueryReferenceResponse<TEntity>>
    where TEntity : BaseEntity;