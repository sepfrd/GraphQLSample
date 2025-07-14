using Domain.Common;

namespace Domain.Entities;

public class Department : BaseEntity
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public ICollection<Employee> Employees { get; set; } = [];
}