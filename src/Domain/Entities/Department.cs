using Domain.Abstractions;

namespace Domain.Entities;

public class Department : DomainEntity
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public ICollection<Employee> Employees { get; set; } = [];
}