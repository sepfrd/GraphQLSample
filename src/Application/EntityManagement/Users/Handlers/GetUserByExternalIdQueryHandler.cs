using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Users.Dtos;
using Application.EntityManagement.Users.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.EntityManagement.Users.Handlers;

public class GetUserByExternalIdQueryHandler : IRequestHandler<GetUserByExternalIdQuery, QueryReferenceResponse<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public GetUserByExternalIdQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<QueryReferenceResponse<UserDto>> Handle(GetUserByExternalIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork
            .UserRepository
            .GetByExternalIdAsync(request.ExternalId, cancellationToken, request.RelationsToInclude);

        if (user is null)
        {
            return new QueryReferenceResponse<UserDto>(
                null,
                true,
                Messages.NotFound,
                HttpStatusCode.NoContent);
        }

        var userDto = _mappingService.Map<User, UserDto>(user);

        if (userDto is not null)
        {
            return new QueryReferenceResponse<UserDto>(
                userDto,
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK);
        }

        _logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(User), typeof(GetUserByExternalIdQueryHandler));

        return new QueryReferenceResponse<UserDto>(
            null,
            false,
            Messages.InternalServerError,
            HttpStatusCode.InternalServerError);
    }
}