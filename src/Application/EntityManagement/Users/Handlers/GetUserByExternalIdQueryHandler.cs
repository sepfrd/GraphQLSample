#region

using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Users.Dtos;
using Application.EntityManagement.Users.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

#endregion

namespace Application.EntityManagement.Users.Handlers;

public class GetUserByExternalIdQueryHandler(
        IRepository<User> userRepository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<GetUserByExternalIdQuery, QueryReferenceResponse<UserDto>>
{
    public async Task<QueryReferenceResponse<UserDto>> Handle(GetUserByExternalIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (user is null)
        {
            return new QueryReferenceResponse<UserDto>(
                null,
                true,
                Messages.NotFound,
                HttpStatusCode.NoContent);
        }

        var userDto = mappingService.Map<User, UserDto>(user);

        if (userDto is not null)
        {
            return new QueryReferenceResponse<UserDto>(
                userDto,
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK);
        }

        logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(User), typeof(GetUserByExternalIdQueryHandler));

        return new QueryReferenceResponse<UserDto>(
            null,
            false,
            Messages.InternalServerError,
            HttpStatusCode.InternalServerError);
    }
}