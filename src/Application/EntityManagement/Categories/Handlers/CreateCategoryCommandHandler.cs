using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Categories.Commands;
using Application.EntityManagement.Categories.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Categories.Handlers;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CommandResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;

    }

    public async Task<CommandResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _mappingService.Map<CategoryDto, Category>(request.CategoryDto);

        if (category is null)
        {
            _logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(Category), typeof(CreateCategoryCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var externalId = await _unitOfWork.CategoryRepository.GenerateUniqueExternalIdAsync(cancellationToken);

        category.ExternalId = externalId;

        await _unitOfWork.CategoryRepository.CreateAsync(category, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult.Success(Messages.SuccessfullyCreated);
    }
}