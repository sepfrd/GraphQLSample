using Application.Common;
using Domain.Entities;

namespace Application.EntityManagement.Roles.Events;

public record RoleDeletedEvent(Role Entity) : EntityDeletedEvent<Role>(Entity);