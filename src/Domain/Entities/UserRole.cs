using Domain.Common;

namespace Domain.Entities;

public class UserRole : BaseEntity
{
    public User? User { get; set; }

    public Guid UserId { get; set; }

    public Role? Role { get; set; }

    public Guid RoleId { get; set; }
}