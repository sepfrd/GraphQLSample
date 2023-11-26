using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Answers.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Answers.Handlers;

public class UpdateAnswerCommandHandler : IRequestHandler<UpdateAnswerCommand, CommandResult>
{
    private readonly IRepository<Answer> _repository;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public UpdateAnswerCommandHandler(IRepository<Answer> repository,
        IMappingService mappingService,
        ILogger logger)
    {
        _repository = repository;
        _mappingService = mappingService;
        _logger = logger;
    }

    public virtual async Task<CommandResult> Handle(UpdateAnswerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Success(Messages.NotFound);
        }

        var newEntity = _mappingService.Map(request.AnswerDto, entity);

        if (newEntity is null)
        {
            _logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(Answer), typeof(UpdateAnswerCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var updatedEntity = await _repository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyUpdated);
        }

        _logger.LogError(message: Messages.EntityUpdateFailed, DateTime.UtcNow, typeof(Answer), typeof(UpdateAnswerCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}