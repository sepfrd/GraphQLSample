using Application.Services.Employees.Dtos;

namespace Application.Services.Projects.Dtos;

public record ProjectDto(
    Guid Id,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt,
    string Name,
    string Description,
    EmployeeDto Manager,
    ICollection<EmployeeDto> Employees);