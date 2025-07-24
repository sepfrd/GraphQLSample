using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Services.Employees.Dtos;

public record CreateEmployeeDto(
    string FirstName,
    string LastName,
    DateTimeOffset BirthDate,
    EmployeeStatus Status,
    string? Position,
    Guid DepartmentId,
    List<Skill> Skills);