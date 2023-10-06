using Application.EntityManagement.Addresses.Dtos;
using Application.EntityManagement.PhoneNumbers.Dtos;

namespace Application.EntityManagement.Users.Dtos;

public sealed record CreateUserDto
(
    string FirstName,
    string LastName,
    DateTime BirthDate,
    string Username,
    string Password,
    string PasswordConfirmation,
    string Email,
    ICollection<AddressDto> Addresses,
    ICollection<PhoneNumberDto> PhoneNumbers
);