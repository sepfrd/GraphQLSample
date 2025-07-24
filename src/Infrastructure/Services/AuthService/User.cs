using Domain.Abstractions;
using Infrastructure.Common.Constants;

namespace Infrastructure.Services.AuthService;

public sealed class User : IEntity<Guid>
{
    public Guid Id { get; init; } = Guid.CreateVersion7();

    public required string Username { get; init; }

    public required string PasswordHash { get; init; }

    public required string Email { get; init; }

    public bool IsEmailConfirmed { get; init; }

    public HashSet<string> Roles { get; set; } = [RoleConstants.User];
}