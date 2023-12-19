using Domain.Common;

namespace Domain.Entities;

public class Role : BaseEntity
{
    public required string Title { get; init; }

    public string? Description { get; init; }

    public ICollection<UserRole>? UserRoles { get; set; }
}