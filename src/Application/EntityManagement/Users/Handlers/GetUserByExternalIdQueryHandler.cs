using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Users.Dtos;
using Application.EntityManagement.Users.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.EntityManagement.Users.Handlers;

public class GetUserByExternalIdQueryHandler(
        IRepository<User> userRepository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<GetUserByExternalIdQuery, QueryResponse<UserDto>>
{
    public async Task<QueryResponse<UserDto>> Handle(GetUserByExternalIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (user is null)
        {
            return new QueryResponse<UserDto>(
                null,
                true,
                MessageConstants.NotFound,
                HttpStatusCode.NoContent);
        }

        var userDto = mappingService.Map<User, UserDto>(user);

        if (userDto is not null)
        {
            return new QueryResponse<UserDto>(
                userDto,
                true,
                MessageConstants.SuccessfullyRetrieved,
                HttpStatusCode.OK);
        }

        logger.LogError(MessageConstants.MappingFailed, DateTime.UtcNow, typeof(User), typeof(GetUserByExternalIdQueryHandler));

        return new QueryResponse<UserDto>(
            null,
            false,
            MessageConstants.InternalServerError,
            HttpStatusCode.InternalServerError);
    }
}