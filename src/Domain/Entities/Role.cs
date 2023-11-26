#region

using Domain.Common;

#endregion

namespace Domain.Entities;

public class Role : BaseEntity
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public ICollection<UserRole>? UserRoles { get; set; }
}