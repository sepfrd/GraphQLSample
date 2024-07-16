using Application.Common;
using Domain.Entities;

namespace Application.EntityManagement.Users.Events;

public record UserDeletedEvent(User Entity) : EntityDeletedEvent<User>(Entity);