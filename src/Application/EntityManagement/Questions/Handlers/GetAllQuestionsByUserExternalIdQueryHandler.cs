using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Questions.Dtos;
using Application.EntityManagement.Questions.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.EntityManagement.Questions.Handlers;

public class GetAllQuestionsByUserExternalIdQueryHandler
    : IRequestHandler<GetAllQuestionsByUserExternalIdQuery, QueryReferenceResponse<IEnumerable<QuestionDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public GetAllQuestionsByUserExternalIdQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<QueryReferenceResponse<IEnumerable<QuestionDto>>> Handle(GetAllQuestionsByUserExternalIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork
            .UserRepository
            .GetByExternalIdAsync(request.UserExternalId, cancellationToken,
                entity => entity.Questions);

        if (user is null)
        {
            return new QueryReferenceResponse<IEnumerable<QuestionDto>>(
                null,
                false,
                Messages.NotFound,
                HttpStatusCode.NotFound);
        }

        if (user.Questions is null)
        {
            _logger.LogError(Messages.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(User), typeof(GetAllQuestionsByUserExternalIdQueryHandler));

            return new QueryReferenceResponse<IEnumerable<QuestionDto>>(
                null,
                false,
                Messages.InternalServerError,
                HttpStatusCode.InternalServerError);
        }

        var questionDtos = _mappingService.Map<ICollection<Question>, ICollection<QuestionDto>>(user.Questions);

        if (questionDtos is not null)
        {
            return new QueryReferenceResponse<IEnumerable<QuestionDto>>(
                questionDtos.Paginate(request.Pagination),
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK);
        }

        _logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(ICollection<Question>), typeof(GetAllQuestionsByUserExternalIdQueryHandler));

        return new QueryReferenceResponse<IEnumerable<QuestionDto>>(
            null,
            false,
            Messages.InternalServerError,
            HttpStatusCode.InternalServerError);
    }
}