using Application.Common;
using Application.EntityManagement.Addresses.Dtos;
using Application.EntityManagement.PhoneNumbers.Dtos;
using Application.EntityManagement.Users.Commands;
using Application.EntityManagement.Users.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Users.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CommandResult>
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var isUsernameUnique = await IsUsernameUniqueAsync(request.UserDto.Username, cancellationToken);

        if (!isUsernameUnique)
        {
            return CommandResult.Failure(Messages.DuplicateUsername);
        }

        var userInternalId = Guid.NewGuid();

        var person = await CreatePersonAsync(request.UserDto, userInternalId, cancellationToken);

        if (person is null)
        {
            _logger.LogError(Messages.EntityCreationFailed, DateTime.UtcNow, typeof(Person), typeof(CreateUserCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var user = await CreateUserAsync(request.UserDto, userInternalId, person.InternalId, cancellationToken);

        if (user is null)
        {
            _logger.LogError(Messages.EntityCreationFailed, DateTime.UtcNow, typeof(User), typeof(CreateUserCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var savingResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (savingResult != 0)
        {
            return CommandResult.Success(Messages.SuccessfullyCreated);
        }

        _logger.LogError(Messages.UnitOfWorkSavingChangesFailed, DateTime.UtcNow, typeof(CreateUserCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }

    private async Task<bool> IsUsernameUniqueAsync(string username, CancellationToken cancellationToken = default)
    {
        var users = await _unitOfWork
            .UserRepository
            .GetAllAsync(user => user.Username == username, cancellationToken);

        return !users.Any();
    }

    private async Task<Person?> CreatePersonAsync(CreateUserDto userDto, Guid userId, CancellationToken cancellationToken = default)
    {
        var externalId = await _unitOfWork.PersonRepository.GenerateUniqueExternalIdAsync(cancellationToken);

        var person = new Person
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            BirthDate = userDto.BirthDate,
            ExternalId = externalId,
            UserId = userId
        };

        var createdPerson = await _unitOfWork.PersonRepository.CreateAsync(person, cancellationToken);

        return createdPerson;
    }

    private async Task<User?> CreateUserAsync(CreateUserDto userDto, Guid userInternalId, Guid personId, CancellationToken cancellationToken = default)
    {
        var externalId = await _unitOfWork.UserRepository.GenerateUniqueExternalIdAsync(cancellationToken);

        var phoneNumberEntities = new List<PhoneNumber>();

        foreach (var phoneNumber in userDto.PhoneNumberDtos)
        {
            var createdPhoneNumber = await CreatePhoneNumberAsync(phoneNumber, userInternalId, cancellationToken);

            if (createdPhoneNumber is not null)
            {
                phoneNumberEntities.Add(createdPhoneNumber);
            }
        }

        var addressEntities = new List<Address>();

        foreach (var address in userDto.AddressDtos)
        {
            var createdAddress = await CreateAddressAsync(address, userInternalId, cancellationToken);

            if (createdAddress is not null)
            {
                addressEntities.Add(createdAddress);
            }
        }

        var user = new User
        {
            InternalId = userInternalId,
            ExternalId = externalId,
            Username = userDto.Username,
            Password = userDto.Password,
            Email = userDto.Email,
            PersonId = personId,
            Addresses = addressEntities,
            PhoneNumbers = phoneNumberEntities
        };

        var createdUser = await _unitOfWork.UserRepository.CreateAsync(user, cancellationToken);

        if (createdUser is not null)
        {
            return createdUser;
        }

        _logger.LogError(Messages.EntityCreationFailed, DateTime.UtcNow, typeof(User), typeof(CreateUserCommandHandler));

        return null;
    }

    private async Task<PhoneNumber?> CreatePhoneNumberAsync(PhoneNumberDto phoneNumberDto, Guid userInternalId, CancellationToken cancellationToken = default)
    {
        var externalId = await _unitOfWork.PhoneNumberRepository.GenerateUniqueExternalIdAsync(cancellationToken);

        var phoneNumber = new PhoneNumber(phoneNumberDto.Number, phoneNumberDto.Type)
        {
            ExternalId = externalId,
            UserId = userInternalId
        };

        var createdPhoneNumber = await _unitOfWork.PhoneNumberRepository.CreateAsync(phoneNumber, cancellationToken);

        return createdPhoneNumber;
    }

    private async Task<Address?> CreateAddressAsync(AddressDto addressDto, Guid userInternalId, CancellationToken cancellationToken = default)
    {
        var externalId = await _unitOfWork.AddressRepository.GenerateUniqueExternalIdAsync(cancellationToken);

        var address = new Address
        {
            Street = addressDto.Street,
            City = addressDto.City,
            State = addressDto.State,
            PostalCode = addressDto.PostalCode,
            Country = addressDto.Country,
            UnitNumber = addressDto.UnitNumber,
            BuildingNumber = addressDto.BuildingNumber,
            ExternalId = externalId,
            UserId = userInternalId
        };

        var createdAddress = await _unitOfWork.AddressRepository.CreateAsync(address, cancellationToken);

        return createdAddress;
    }
}