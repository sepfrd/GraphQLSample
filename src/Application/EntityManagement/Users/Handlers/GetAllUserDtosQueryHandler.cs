using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Addresses.Dtos;
using Application.EntityManagement.PhoneNumbers.Dtos;
using Application.EntityManagement.Users.Dtos;
using Application.EntityManagement.Users.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Users.Handlers;

public class GetAllUserDtosQueryHandler : IRequestHandler<GetAllUserDtosQuery, QueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;

    public GetAllUserDtosQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
    }

    public async Task<QueryResponse> Handle(GetAllUserDtosQuery request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync(cancellationToken);

        var usersList = users.ToList();

        if (usersList.Count == 0)
        {
            return new QueryResponse
                (
                users,
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK
                );
        }

        var userDtos = new List<UserDto>();

        foreach (var user in usersList)
        {
            var person = await _unitOfWork.PersonRepository.GetByInternalIdAsync(user.PersonId, cancellationToken);
            var phoneNumbers = await _unitOfWork.UserRepository.GetAllPhoneNumbersByInternalIdsAsync(user.PhoneNumberIds, cancellationToken);
            var addresses = await _unitOfWork.UserRepository.GetAllAddressesByInternalIdsAsync(user.AddressIds, cancellationToken);

            var phoneNumberDtos = _mappingService.Map<IEnumerable<PhoneNumber>, IEnumerable<PhoneNumberDto>>(phoneNumbers);
            var addressDtos = _mappingService.Map<IEnumerable<Address>, IEnumerable<AddressDto>>(addresses);

            var userDto = new UserDto
                (
                person?.FirstName ?? string.Empty,
                person?.LastName ?? string.Empty,
                user.Username,
                user.Email,
                user.Score,
                user.OrderIds?.Count ?? 0,
                user.QuestionIds?.Count ?? 0,
                user.AnswerIds?.Count ?? 0,
                user.VoteIds?.Count ?? 0,
                addressDtos,
                phoneNumberDtos
                );

            userDtos.Add(userDto);
        }

        return new QueryResponse
            (
            userDtos,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}