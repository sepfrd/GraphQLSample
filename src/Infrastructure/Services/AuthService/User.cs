using Domain.Abstractions;

namespace Infrastructure.Services.AuthService;

public sealed class User : IHasUuid
{
    public Guid Uuid { get; init; } = Guid.CreateVersion7();

    public required string Username { get; init; }

    public required string PasswordHash { get; init; }

    public required string Email { get; init; }

    public bool IsEmailConfirmed { get; init; }
}