using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Answers.Dtos;
using Application.EntityManagement.Answers.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.EntityManagement.Answers.Handlers;

public class GetAllAnswersByQuestionExternalIdQueryHandler
    : IRequestHandler<GetAllAnswersByQuestionExternalIdQuery, QueryReferenceResponse<IEnumerable<AnswerDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public GetAllAnswersByQuestionExternalIdQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<QueryReferenceResponse<IEnumerable<AnswerDto>>> Handle(GetAllAnswersByQuestionExternalIdQuery request, CancellationToken cancellationToken)
    {
        var question = await _unitOfWork
            .QuestionRepository
            .GetByExternalIdAsync(request.QuestionExternalId, cancellationToken,
                entity => entity.Answers);

        if (question is null)
        {
            return new QueryReferenceResponse<IEnumerable<AnswerDto>>
                (
                null,
                false,
                Messages.NotFound,
                HttpStatusCode.NotFound
                );
        }

        if (question.Answers is null)
        {
            _logger.LogError(Messages.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(Question), typeof(GetAllAnswersByQuestionExternalIdQueryHandler));

            return new QueryReferenceResponse<IEnumerable<AnswerDto>>
                (
                null,
                false,
                Messages.InternalServerError,
                HttpStatusCode.InternalServerError
                );
        }

        var answerDtos = _mappingService.Map<ICollection<Answer>, ICollection<AnswerDto>>(question.Answers);

        if (answerDtos is not null)
        {
            return new QueryReferenceResponse<IEnumerable<AnswerDto>>
                (
                answerDtos.Paginate(request.Pagination),
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK
                );
        }

        _logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(ICollection<Answer>), typeof(GetAllAnswersByQuestionExternalIdQueryHandler));

        return new QueryReferenceResponse<IEnumerable<AnswerDto>>
            (
            null,
            false,
            Messages.InternalServerError,
            HttpStatusCode.InternalServerError
            );
    }
}