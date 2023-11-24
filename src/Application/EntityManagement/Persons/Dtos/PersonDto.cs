namespace Application.EntityManagement.Persons.Dtos;

public sealed record PersonDto(
    string FirstName,
    string LastName,
    int Age,
    DateTime BirthDate);