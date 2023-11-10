using Application.EntityManagement.Users.Dtos;

namespace Application.EntityManagement.Persons.Dtos;

public record PersonDto
(
    int ExternalId,
    string FirstName,
    string LastName,
    int Age,
    DateTime BirthDate,
    UserDto? UserDto
);