using Domain.Abstractions;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Employee : DomainEntity
{
    public required PersonInfo Info { get; set; }

    public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;

    public string? Position { get; set; }

    public Guid DepartmentId { get; set; }

    public HashSet<Guid> ProjectIds { get; set; } = [];

    public HashSet<Skill> Skills { get; set; } = [];
}