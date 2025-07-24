using Application.Services.Departments.Dtos;
using Application.Services.Projects.Dtos;
using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Services.Employees.Dtos;

public record EmployeeDto(
    Guid Id,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt,
    string FirstName,
    string LastName,
    int Age,
    EmployeeStatus Status,
    string? Position,
    List<Skill> Skills,
    DepartmentDto Department,
    IEnumerable<ProjectDto> Projects);