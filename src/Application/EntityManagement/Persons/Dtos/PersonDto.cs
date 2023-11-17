namespace Application.EntityManagement.Persons.Dtos;

public sealed record PersonDto(
    int ExternalId,
    string FirstName,
    string LastName,
    int Age,
    DateTime BirthDate);