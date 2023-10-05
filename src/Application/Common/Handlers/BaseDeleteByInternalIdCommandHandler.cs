using Application.Abstractions;
using Application.Common.Commands;
using Domain.Abstractions;
using Domain.Common;
using MediatR;

namespace Application.Common.Handlers;

public abstract class BaseDeleteByInternalIdCommandHandler<TEntity, TDto>
    : IRequestHandler<BaseDeleteByInternalIdCommand<TDto>, TDto?>
    where TEntity : BaseEntity
    where TDto : class
{
    private readonly IRepository<TEntity> _repository;
    private readonly IMappingService _mappingService;

    protected BaseDeleteByInternalIdCommandHandler(IRepository<TEntity> repository, IMappingService mappingService)
    {
        _repository = repository;
        _mappingService = mappingService;

    }

    public async Task<TDto?> Handle(BaseDeleteByInternalIdCommand<TDto> request, CancellationToken cancellationToken)
    {
        var deletedEntity = await _repository.DeleteByInternalIdAsync(request.Id, cancellationToken);

        if (deletedEntity is null)
        {
            return null;
        }

        var deletedEntityDto = _mappingService.Map<TEntity, TDto>(deletedEntity);

        return deletedEntityDto;
    }
}