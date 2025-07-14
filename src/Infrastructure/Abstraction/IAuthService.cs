using Infrastructure.Services.AuthService;
using Infrastructure.Services.AuthService.Dtos;

namespace Infrastructure.Abstraction;

public interface IAuthService
{
    Task<string?> CreateJwtAsync(User user, CancellationToken cancellationToken = default);

    UserClaimsDto? GetLoggedInUser();

    string HashPassword(string password);

    bool ValidatePassword(string password, string passwordHash);

    bool IsLoggedIn();
}