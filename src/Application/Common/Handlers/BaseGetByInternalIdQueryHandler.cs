using Application.Common.Queries;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using System.Net;

namespace Application.Common.Handlers;

public abstract class BaseGetByInternalIdQueryHandler<TEntity>
    : IRequestHandler<BaseGetByInternalIdQuery, QueryResponse>
    where TEntity : BaseEntity
{
    private readonly IRepository<TEntity> _repository;

    protected BaseGetByInternalIdQueryHandler(IRepository<TEntity> repository) =>
        _repository = repository;

    public async Task<QueryResponse> Handle(BaseGetByInternalIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByInternalIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return new QueryResponse(Message: Messages.NotFound, HttpStatusCode: HttpStatusCode.NotFound);
        }

        return new QueryResponse
            (
            entity,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}