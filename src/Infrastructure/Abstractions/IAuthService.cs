using Domain.Abstractions;
using Infrastructure.Services.AuthService;
using Infrastructure.Services.AuthService.Dtos;
using Infrastructure.Services.AuthService.Dtos.LoginDto;

namespace Infrastructure.Abstractions;

public interface IAuthService
{
    Task<DomainResult<string>> LogInAsync(LoginDto loginDto, CancellationToken cancellationToken = default);

    string GenerateAuthToken(User user, CancellationToken cancellationToken = default);

    UserClaimsDto? GetLoggedInUser();

    string HashPassword(string password);

    bool ValidatePassword(string password, string passwordHash);

    bool IsLoggedIn();
}