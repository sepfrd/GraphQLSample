using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Addresses.Dtos;
using Application.EntityManagement.Addresses.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.EntityManagement.Addresses.Handlers;

public class GetAllAddressesByUserExternalIdQueryHandler : IRequestHandler<GetAllAddressesByUserExternalIdQuery, QueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public GetAllAddressesByUserExternalIdQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<QueryResponse> Handle(GetAllAddressesByUserExternalIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork
            .UserRepository
            .GetByExternalIdAsync(request.UserExternalId, cancellationToken,
                entity => entity.Addresses);

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

        if (user.Addresses is null)
        {
            _logger.LogError(Messages.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(User), typeof(GetAllAddressesByUserExternalIdQueryHandler));

            return new QueryResponse
                (
                null,
                false,
                Messages.InternalServerError,
                HttpStatusCode.InternalServerError
                );
        }

        var addressDtos = _mappingService.Map<ICollection<Address>, ICollection<AddressDto>>(user.Addresses);

        if (addressDtos is not null)
        {
            return new QueryResponse
                (
                addressDtos.Paginate(request.Pagination),
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK
                );
        }

        _logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(ICollection<Address>), typeof(GetAllAddressesByUserExternalIdQueryHandler));

        return new QueryResponse
            (
            null,
            false,
            Messages.InternalServerError,
            HttpStatusCode.InternalServerError
            );
    }
}