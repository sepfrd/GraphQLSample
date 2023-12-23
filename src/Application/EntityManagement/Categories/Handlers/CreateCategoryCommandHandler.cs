using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Categories.Commands;
using Application.EntityManagement.Categories.Dtos;
using Application.EntityManagement.Categories.Dtos.CategoryDto;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Categories.Handlers;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CommandResult>
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public CreateCategoryCommandHandler(
        IRepository<Category> categoryRepository,
        IMappingService mappingService,
        ILogger logger)
    {
        _categoryRepository = categoryRepository;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _mappingService.Map<CategoryDto, Category>(request.CategoryDto);

        if (category is null)
        {
            _logger.LogError(MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Category), typeof(CreateCategoryCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        await _categoryRepository.CreateAsync(category, cancellationToken);

        return CommandResult.Success(MessageConstants.SuccessfullyCreated);
    }
}