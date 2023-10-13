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

public class GetAllUserDtosQueryHandler : IRequestHandler<GetAllUserDtosQuery, QueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public GetAllUserDtosQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<QueryResponse> Handle(GetAllUserDtosQuery request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync(null, request.RelationsToInclude, cancellationToken);

        var usersList = users.Paginate(request.Pagination);

        if (usersList.Count == 0)
        {
            return new QueryResponse
                (
                Array.Empty<UserDto>(),
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK
                );
        }

        var userDtos = _mappingService.Map<List<User>, List<UserDto>>(usersList);

        if (userDtos is not null)
        {
            return new QueryResponse
                (
                userDtos,
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK
                );
        }

        _logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(User), typeof(GetAllUserDtosQueryHandler));

        return new QueryResponse
            (
            null,
            false,
            Messages.InternalServerError,
            HttpStatusCode.InternalServerError
            );
    }
}