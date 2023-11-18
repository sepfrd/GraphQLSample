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

public class CreateUserCommandHandler(
        IRepository<User> userRepository,
        IRepository<Person> personRepository,
        IRepository<PhoneNumber> phoneNumberRepository,
        IRepository<Address> addressRepository,
        ILogger logger)
    : IRequestHandler<CreateUserCommand, CommandResult>
{
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
            logger.LogError(Messages.EntityCreationFailed, DateTime.UtcNow, typeof(Person), typeof(CreateUserCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var user = await CreateUserAsync(request.UserDto, userInternalId, person.InternalId, cancellationToken);

        if (user is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyCreated);
        }

        logger.LogError(Messages.EntityCreationFailed, DateTime.UtcNow, typeof(User), typeof(CreateUserCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }

    private async Task<bool> IsUsernameUniqueAsync(string username, CancellationToken cancellationToken = default)
    {
        var users = await userRepository.GetAllAsync(
            user => user.Username == username,
            cancellationToken);

        return !users.Any();
    }

    private async Task<Person?> CreatePersonAsync(CreateUserDto userDto, Guid userId, CancellationToken cancellationToken = default)
    {
        var externalId = await personRepository.GenerateUniqueExternalIdAsync(cancellationToken);

        var person = new Person
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            BirthDate = userDto.BirthDate,
            ExternalId = externalId,
            UserId = userId
        };

        var createdPerson = await personRepository.CreateAsync(person, cancellationToken);

        return createdPerson;
    }

    private async Task<User?> CreateUserAsync(CreateUserDto userDto, Guid userInternalId, Guid personId, CancellationToken cancellationToken = default)
    {
        var externalId = await userRepository.GenerateUniqueExternalIdAsync(cancellationToken);

        var phoneNumberEntities = new List<PhoneNumber>();

        foreach (var phoneNumber in userDto.PhoneNumbers)
        {
            var createdPhoneNumber = await CreatePhoneNumberAsync(phoneNumber, userInternalId, cancellationToken);

            if (createdPhoneNumber is not null)
            {
                phoneNumberEntities.Add(createdPhoneNumber);
            }
        }

        var addressEntities = new List<Address>();

        foreach (var address in userDto.Addresses)
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

        var createdUser = await userRepository.CreateAsync(user, cancellationToken);

        if (createdUser is not null)
        {
            return createdUser;
        }

        logger.LogError(Messages.EntityCreationFailed, DateTime.UtcNow, typeof(User), typeof(CreateUserCommandHandler));

        return null;
    }

    private async Task<PhoneNumber?> CreatePhoneNumberAsync(PhoneNumberDto phoneNumberDto, Guid userInternalId, CancellationToken cancellationToken = default)
    {
        var externalId = await phoneNumberRepository.GenerateUniqueExternalIdAsync(cancellationToken);

        var phoneNumber = new PhoneNumber
        {
            Number = phoneNumberDto.Number,
            Type = phoneNumberDto.Type,
            ExternalId = externalId,
            UserId = userInternalId
        };

        var createdPhoneNumber = await phoneNumberRepository.CreateAsync(phoneNumber, cancellationToken);

        return createdPhoneNumber;
    }

    private async Task<Address?> CreateAddressAsync(AddressDto addressDto, Guid userInternalId, CancellationToken cancellationToken = default)
    {
        var externalId = await addressRepository.GenerateUniqueExternalIdAsync(cancellationToken);

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

        var createdAddress = await addressRepository.CreateAsync(address, cancellationToken);

        return createdAddress;
    }
}