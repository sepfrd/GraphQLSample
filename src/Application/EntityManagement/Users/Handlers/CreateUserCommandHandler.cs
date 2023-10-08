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

        var person = await CreatePersonAsync(request.UserDto, cancellationToken);

        if (person is null)
        {
            _logger.LogError(Messages.EntityCreationFailed, DateTime.UtcNow, typeof(Person));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var user = await CreateUserAsync(request.UserDto, person.InternalId, cancellationToken);

        if (user is null)
        {
            _logger.LogError(Messages.EntityCreationFailed, DateTime.UtcNow, typeof(User));
            
            return CommandResult.Failure(Messages.InternalServerError);
        }

        var savingResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (savingResult == 0)
        {
            _logger.LogError(Messages.UnitOfWorkSavingChangesFailed, DateTime.UtcNow, typeof(CreateUserCommandHandler));
            
            return CommandResult.Failure(Messages.InternalServerError);
        }
        
        return CommandResult.Success(Messages.SuccessfullyCreated);
    }

    private async Task<bool> IsUsernameUniqueAsync(string username, CancellationToken cancellationToken = default)
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync(cancellationToken);

        var existingUser = users.Where(user => user.Username == username);

        return !existingUser.Any();
    }

    private async Task<Person?> CreatePersonAsync(CreateUserDto userDto, CancellationToken cancellationToken = default)
    {
        var externalId = await _unitOfWork.PersonRepository.GenerateUniqueExternalIdAsync(cancellationToken);

        var person = new Person
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            BirthDate = userDto.BirthDate,
            ExternalId = externalId
        };

        var createdPerson = await _unitOfWork.PersonRepository.CreateAsync(person, cancellationToken);

        return createdPerson;
    }

    private async Task<User?> CreateUserAsync(CreateUserDto userDto, Guid personId, CancellationToken cancellationToken = default)
    {
        var externalId = await _unitOfWork.UserRepository.GenerateUniqueExternalIdAsync(cancellationToken);

        var phoneNumbersEntities = new List<PhoneNumber>();
        var addressesEntities = new List<Address>();

        foreach (var phoneNumber in userDto.PhoneNumbers)
        {
            var phoneNumberEntity = await CreatePhoneNumberAsync(phoneNumber, cancellationToken);

            if (phoneNumberEntity is null)
            {
                _logger.LogError(Messages.EntityCreationFailed, DateTime.UtcNow, typeof(PhoneNumber));
            }
            else
            {
                phoneNumbersEntities.Add(phoneNumberEntity);
            }
        }

        foreach (var address in userDto.Addresses)
        {
            var addressEntity = await CreateAddressAsync(address, cancellationToken);

            if (addressEntity is null)
            {
                _logger.LogError(Messages.EntityCreationFailed, DateTime.UtcNow, typeof(Address));
            }
            else
            {
                addressesEntities.Add(addressEntity);
            }
        }

        var phoneNumberIds = phoneNumbersEntities.Select(phoneNumber => phoneNumber.InternalId).ToList();

        var addressIds = addressesEntities.Select(address => address.InternalId).ToList();

        var user = new User
        {
            Username = userDto.Username,
            Password = userDto.Password,
            Email = userDto.Email,
            PersonId = personId,
            PhoneNumberIds = phoneNumberIds,
            AddressIds = addressIds,
            ExternalId = externalId
        };

        var createdUser = await _unitOfWork.UserRepository.CreateAsync(user, cancellationToken);
        
        return createdUser;
    }

    private async Task<PhoneNumber?> CreatePhoneNumberAsync(PhoneNumberDto phoneNumberDto, CancellationToken cancellationToken = default)
    {
        var externalId = await _unitOfWork.PhoneNumberRepository.GenerateUniqueExternalIdAsync(cancellationToken);

        var phoneNumber = new PhoneNumber(phoneNumberDto.Number, phoneNumberDto.Type)
        {
            ExternalId = externalId
        };

        var createdPhoneNumber = await _unitOfWork.PhoneNumberRepository.CreateAsync(phoneNumber, cancellationToken);

        return createdPhoneNumber;
    }

    private async Task<Address?> CreateAddressAsync(AddressDto addressDto, CancellationToken cancellationToken = default)
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
            ExternalId = externalId
        };

        var createdAddress = await _unitOfWork.AddressRepository.CreateAsync(address, cancellationToken);

        return createdAddress;
    }
}