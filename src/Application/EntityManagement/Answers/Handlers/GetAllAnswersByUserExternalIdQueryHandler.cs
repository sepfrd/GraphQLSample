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

public class GetAllAnswersByUserExternalIdQueryHandler
    : IRequestHandler<GetAllAnswersByUserExternalIdQuery, QueryResponse>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public GetAllAnswersByUserExternalIdQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<QueryResponse> Handle(GetAllAnswersByUserExternalIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByExternalIdAsync(request.UserExternalId,
            new Func<User, object?>[]
            {
                entity => entity.Answers
            },
            cancellationToken);

        if (user is null)
        {
            return new QueryResponse
                (
                null,
                false,
                Messages.NotFound,
                HttpStatusCode.NotFound
                );
        }

        if (user.Answers is null)
        {
            _logger.LogError(Messages.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(User), typeof(GetAllAnswersByUserExternalIdQueryHandler));

            return new QueryResponse
                (
                null,
                false,
                Messages.InternalServerError,
                HttpStatusCode.InternalServerError
                );
        }

        var answerDtos = _mappingService.Map<ICollection<Answer>, ICollection<AnswerDto>>(user.Answers);

        if (answerDtos is not null)
        {
            return new QueryResponse
                (
                answerDtos.Paginate(request.Pagination),
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK
                );
        }

        _logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(ICollection<Answer>), typeof(GetAllAnswersByUserExternalIdQueryHandler));

        return new QueryResponse
            (
            null,
            false,
            Messages.InternalServerError,
            HttpStatusCode.InternalServerError
            );
    }
}