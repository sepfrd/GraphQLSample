using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Employee : BaseEntity
{
    public required PersonInfo Info { get; set; }

    public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;

    public string? Position { get; set; }

    public Guid DepartmentUuid { get; set; }

    public Department? Department { get; set; }

    public List<Skill> Skills { get; set; } = [];
}