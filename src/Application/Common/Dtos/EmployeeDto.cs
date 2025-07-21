using Domain.Abstractions;
using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Common.Dtos;

public record EmployeeDto(
    Guid Uuid,
    string FirstName,
    string LastName,
    EmployeeStatus Status,
    string? Position,
    Guid DepartmentUuid,
    List<Skill> Skills) : IHasUuid;