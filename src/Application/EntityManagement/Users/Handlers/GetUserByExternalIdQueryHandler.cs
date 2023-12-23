using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Users.Dtos;
using Application.EntityManagement.Users.Dtos.UserDto;
using Application.EntityManagement.Users.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.EntityManagement.Users.Handlers;

public class GetUserByExternalIdQueryHandler : IRequestHandler<GetUserByExternalIdQuery, QueryResponse<UserDto>>
{
    private readonly IRepository<User> _userRepository;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public GetUserByExternalIdQueryHandler(
        IRepository<User> userRepository,
        IMappingService mappingService,
        ILogger logger)
    {
        _userRepository = userRepository;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<QueryResponse<UserDto>> Handle(GetUserByExternalIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (user is null)
        {
            return new QueryResponse<UserDto>(
                null,
                true,
                MessageConstants.NotFound,
                HttpStatusCode.NoContent);
        }

        var userDto = _mappingService.Map<User, UserDto>(user);

        if (userDto is not null)
        {
            return new QueryResponse<UserDto>(
                userDto,
                true,
                MessageConstants.SuccessfullyRetrieved,
                HttpStatusCode.OK);
        }

        _logger.LogError(MessageConstants.MappingFailed, DateTime.UtcNow, typeof(User), typeof(GetUserByExternalIdQueryHandler));

        return new QueryResponse<UserDto>(
            null,
            false,
            MessageConstants.InternalServerError,
            HttpStatusCode.InternalServerError);
    }
}