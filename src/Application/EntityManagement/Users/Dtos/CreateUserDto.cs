#region

using Application.EntityManagement.Addresses.Dtos;
using Application.EntityManagement.PhoneNumbers.Dtos;

#endregion

namespace Application.EntityManagement.Users.Dtos;

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