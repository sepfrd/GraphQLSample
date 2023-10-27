using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Questions.Dtos;
using Application.EntityManagement.Questions.Queries;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.EntityManagement.Questions.Handlers;

public class GetAllQuestionsByProductExternalIdQueryHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public GetAllQuestionsByProductExternalIdQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<QueryResponse> Handle(GetAllQuestionsByProductExternalIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork
            .ProductRepository
            .GetByExternalIdAsync(request.ProductExternalId, cancellationToken,
                entity => entity.Questions);

        if (product is null)
        {
            return new QueryResponse
                (
                null,
                false,
                Messages.NotFound,
                HttpStatusCode.NotFound
                );
        }

        if (product.Questions is null)
        {
            _logger.LogError(Messages.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(Product), typeof(GetAllQuestionsByProductExternalIdQueryHandler));

            return new QueryResponse
                (
                null,
                false,
                Messages.InternalServerError,
                HttpStatusCode.InternalServerError
                );
        }

        var questionDtos = _mappingService.Map<ICollection<Question>, ICollection<QuestionDto>>(product.Questions);

        if (questionDtos is not null)
        {
            return new QueryResponse
                (
                questionDtos.Paginate(request.Pagination),
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK
                );
        }

        _logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(ICollection<Question>), typeof(GetAllQuestionsByProductExternalIdQueryHandler));

        return new QueryResponse
            (
            null,
            false,
            Messages.InternalServerError,
            HttpStatusCode.InternalServerError
            );
    }
}