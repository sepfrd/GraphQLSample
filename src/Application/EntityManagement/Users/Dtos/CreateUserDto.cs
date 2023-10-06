using Application.EntityManagement.PhoneNumbers.Dtos;

namespace Application.EntityManagement.Users.Dtos;

public record CreateUserDto
(
    string FirstName,
    string LastName,
    DateTime BirthDate,
    string Username,
    string Password,
    string PasswordConfirmation,
    ICollection<PhoneNumberDto> PhoneNumbers
);