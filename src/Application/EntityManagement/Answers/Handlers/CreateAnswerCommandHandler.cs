using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.Answers.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Answers.Handlers;

public class CreateAnswerCommandHandler(
        IRepository<Answer> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<CreateAnswerCommand, CommandResult>
{
    public async Task<CommandResult> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
    {
        var entity = mappingService.Map<AnswerDto, Answer>(request.AnswerDto);

        if (entity is null)
        {
            logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(Answer), typeof(CreateAnswerCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var createdEntity = await repository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyCreated);
        }

        logger.LogError(message: Messages.EntityCreationFailed, DateTime.UtcNow, typeof(Answer), typeof(CreateAnswerCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}