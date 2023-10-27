using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.PhoneNumbers.Dtos;
using Application.EntityManagement.PhoneNumbers.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.EntityManagement.PhoneNumbers.Handlers;

public class GetAllPhoneNumbersByUserExternalIdQueryHandler : IRequestHandler<GetAllPhoneNumbersByUserExternalIdQuery, QueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public GetAllPhoneNumbersByUserExternalIdQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<QueryResponse> Handle(GetAllPhoneNumbersByUserExternalIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork
            .UserRepository
            .GetByExternalIdAsync(request.UserExternalId, cancellationToken,
                entity => entity.PhoneNumbers);

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

        if (user.PhoneNumbers is null)
        {
            _logger.LogError(Messages.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(User), typeof(GetAllPhoneNumbersByUserExternalIdQueryHandler));

            return new QueryResponse
                (
                null,
                false,
                Messages.InternalServerError,
                HttpStatusCode.InternalServerError
                );
        }

        var phoneNumberDtos = _mappingService.Map<ICollection<PhoneNumber>, ICollection<PhoneNumberDto>>(user.PhoneNumbers);

        if (phoneNumberDtos is not null)
        {
            return new QueryResponse
                (
                phoneNumberDtos.Paginate(request.Pagination),
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK
                );
        }

        _logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(ICollection<PhoneNumber>), typeof(GetAllPhoneNumbersByUserExternalIdQueryHandler));

        return new QueryResponse
            (
            null,
            false,
            Messages.InternalServerError,
            HttpStatusCode.InternalServerError
            );
    }
}