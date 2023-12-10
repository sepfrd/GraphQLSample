namespace Application.EntityManagement.Persons.Dtos;

public sealed record UpdatePersonDto(
    string? FirstName,
    string? LastName,
    DateTime? BirthDate);