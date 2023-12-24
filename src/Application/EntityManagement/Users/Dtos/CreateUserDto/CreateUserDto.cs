using Application.EntityManagement.Addresses.Dtos.AddressDto;
using Application.EntityManagement.PhoneNumbers.Dtos.PhoneNumberDto;

namespace Application.EntityManagement.Users.Dtos.CreateUserDto;

public sealed record CreateUserDto(
    string FirstName,
    string LastName,
    DateTime BirthDate,
    string Username,
    string Password,
    string PasswordConfirmation,
    string Email,
    IEnumerable<AddressDto> Addresses,
    IEnumerable<PhoneNumberDto> PhoneNumbers);