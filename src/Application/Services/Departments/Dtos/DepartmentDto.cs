using Application.Services.Employees.Dtos;

namespace Application.Services.Departments.Dtos;

public record DepartmentDto(
    Guid Id,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt,
    string Name,
    string? Description,
    ICollection<EmployeeDto> Employees);