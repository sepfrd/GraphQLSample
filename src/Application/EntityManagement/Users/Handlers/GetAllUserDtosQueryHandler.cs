using System.Net;
using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Users.Dtos;
using Application.EntityManagement.Users.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Users.Handlers;

public class GetAllUserDtosQueryHandler(
        IRepository<User> userRepository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<GetAllUserDtosQuery, QueryReferenceResponse<IEnumerable<UserDto>>>
{
    public async Task<QueryReferenceResponse<IEnumerable<UserDto>>> Handle(GetAllUserDtosQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAllAsync(null, cancellationToken);

        var usersList = users.Paginate(request.Pagination);

        if (usersList.Count == 0)
        {
            return new QueryReferenceResponse<IEnumerable<UserDto>>(
                Array.Empty<UserDto>(),
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK);
        }

        var userDtos = mappingService.Map<List<User>, List<UserDto>>(usersList);

        if (userDtos is not null)
        {
            return new QueryReferenceResponse<IEnumerable<UserDto>>(
                userDtos,
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK);
        }

        logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(User), typeof(GetAllUserDtosQueryHandler));

        return new QueryReferenceResponse<IEnumerable<UserDto>>(
            null,
            false,
            Messages.InternalServerError,
            HttpStatusCode.InternalServerError);
    }
}