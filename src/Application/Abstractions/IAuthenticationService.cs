using Domain.Entities;

namespace Application.Abstractions;

public interface IAuthenticationService
{
    Task<string?> CreateJwtAsync(User user, CancellationToken cancellationToken = default);
}