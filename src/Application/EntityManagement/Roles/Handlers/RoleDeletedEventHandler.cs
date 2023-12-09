using Application.EntityManagement.Roles.Events;
using Domain.Abstractions;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Roles.Handlers;

public class RoleDeletedEventHandler : INotificationHandler<RoleDeletedEvent>
{
    private readonly IRepository<UserRole> _userRoleRepository;

    public RoleDeletedEventHandler(IRepository<UserRole> userRoleRepository)
    {
        _userRoleRepository = userRoleRepository;
    }

    public async Task Handle(RoleDeletedEvent notification, CancellationToken cancellationToken)
    {
        var userRoles = (await _userRoleRepository.GetAllAsync(
                userRole => userRole.RoleId == notification.Entity.InternalId,
                Pagination.MaxPagination,
                cancellationToken))
            .ToList();

        if (userRoles.Count != 0)
        {
            await _userRoleRepository.DeleteManyAsync(userRoles, cancellationToken);
        }
    }
}