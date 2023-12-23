using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Addresses.Dtos.AddressDto;
using Application.EntityManagement.PhoneNumbers.Dtos;
using Application.EntityManagement.PhoneNumbers.Dtos.PhoneNumberDto;
using Application.EntityManagement.Users.Commands;
using Application.EntityManagement.Users.Dtos;
using Application.EntityManagement.Users.Dtos.CreateUserDto;
using Domain.Abstractions;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Users.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CommandResult>
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Person> _personRepository;
    private readonly IRepository<PhoneNumber> _phoneNumberRepository;
    private readonly IRepository<Address> _addressRepository;
    private readonly ILogger _logger;

    public CreateUserCommandHandler(
        IRepository<User> userRepository,
        IRepository<Person> personRepository,
        IRepository<PhoneNumber> phoneNumberRepository,
        IRepository<Address> addressRepository,
        ILogger logger)
    {
        _userRepository = userRepository;
        _personRepository = personRepository;
        _phoneNumberRepository = phoneNumberRepository;
        _addressRepository = addressRepository;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var isUsernameUnique = await IsUsernameUniqueAsync(request.UserDto.Username, cancellationToken);

        if (!isUsernameUnique)
        {
            return CommandResult.Failure(MessageConstants.DuplicateUsername);
        }

        var userInternalId = Guid.NewGuid();

        var person = await CreatePersonAsync(request.UserDto, userInternalId, cancellationToken);

        if (person is null)
        {
            _logger.LogError(MessageConstants.EntityCreationFailed, DateTime.UtcNow, typeof(Person), typeof(CreateUserCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var user = await CreateUserAsync(request.UserDto, userInternalId, person.InternalId, cancellationToken);

        if (user is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyCreated);
        }

        _logger.LogError(MessageConstants.EntityCreationFailed, DateTime.UtcNow, typeof(User), typeof(CreateUserCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }

    private async Task<bool> IsUsernameUniqueAsync(string username, CancellationToken cancellationToken = default)
    {
        var pagination = new Pagination(1, 1);

        var users = await _userRepository.GetAllAsync(
            user => user.Username == username,
            pagination,
            cancellationToken);

        return !users.Any();
    }

    private async Task<Person?> CreatePersonAsync(CreateUserDto userDto, Guid userId, CancellationToken cancellationToken = default)
    {
        var person = new Person
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            BirthDate = userDto.BirthDate,
            UserId = userId
        };

        var createdPerson = await _personRepository.CreateAsync(person, cancellationToken);

        return createdPerson;
    }

    private async Task<User?> CreateUserAsync(CreateUserDto userDto, Guid userInternalId, Guid personId, CancellationToken cancellationToken = default)
    {
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
            Username = userDto.Username,
            Password = userDto.Password,
            Email = userDto.Email,
            PersonId = personId,
            Addresses = addressEntities,
            PhoneNumbers = phoneNumberEntities
        };

        var createdUser = await _userRepository.CreateAsync(user, cancellationToken);

        if (createdUser is not null)
        {
            return createdUser;
        }

        _logger.LogError(MessageConstants.EntityCreationFailed, DateTime.UtcNow, typeof(User), typeof(CreateUserCommandHandler));

        return null;
    }

    private async Task<PhoneNumber?> CreatePhoneNumberAsync(PhoneNumberDto phoneNumberDto, Guid userInternalId, CancellationToken cancellationToken = default)
    {
        var phoneNumber = new PhoneNumber
        {
            Number = phoneNumberDto.Number,
            Type = phoneNumberDto.Type,
            UserId = userInternalId
        };

        var createdPhoneNumber = await _phoneNumberRepository.CreateAsync(phoneNumber, cancellationToken);

        return createdPhoneNumber;
    }

    private async Task<Address?> CreateAddressAsync(AddressDto addressDto, Guid userInternalId, CancellationToken cancellationToken = default)
    {
        var address = new Address
        {
            Street = addressDto.Street,
            City = addressDto.City,
            State = addressDto.State,
            PostalCode = addressDto.PostalCode,
            Country = addressDto.Country,
            UnitNumber = addressDto.UnitNumber,
            BuildingNumber = addressDto.BuildingNumber,
            UserId = userInternalId
        };

        var createdAddress = await _addressRepository.CreateAsync(address, cancellationToken);

        return createdAddress;
    }
}