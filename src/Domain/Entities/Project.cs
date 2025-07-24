using Domain.Abstractions;

namespace Domain.Entities;

public class Project : DomainEntity
{
    public required string Name { get; set; }

    public required string Description { get; set; }

    public Guid ManagerId { get; set; }

    public ICollection<Guid> EmployeeIds { get; set; } = [];
}