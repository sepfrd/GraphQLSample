using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Services.Employees.Dtos;

public record UpdateEmployeeDto(
    Guid Id,
    string FirstName,
    string LastName,
    DateTimeOffset BirthDate,
    EmployeeStatus Status,
    string? Position,
    List<Skill> Skills);