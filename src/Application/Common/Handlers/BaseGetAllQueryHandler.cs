using Application.Common.Queries;
using Domain.Abstractions;
using Domain.Common;
using MediatR;

namespace Application.Common.Handlers;

public abstract class BaseGetAllQueryHandler<TEntity>
    : IRequestHandler<BaseGetAllQuery<TEntity>, IEnumerable<TEntity>>
    where TEntity : BaseEntity
{
    private readonly IRepository<TEntity> _repository;

    protected BaseGetAllQueryHandler(IRepository<TEntity> repository) =>
        _repository = repository;


    public async Task<IEnumerable<TEntity>> Handle(BaseGetAllQuery<TEntity> request, CancellationToken cancellationToken) =>
        await _repository.GetAllAsync(cancellationToken);
}