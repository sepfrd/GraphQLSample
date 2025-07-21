using Infrastructure.Common.Dtos;
using Infrastructure.Services.AuthService;
using Infrastructure.Services.AuthService.Dtos;
using Infrastructure.Services.AuthService.Dtos.LoginDto;

namespace Infrastructure.Abstraction;

public interface IAuthService
{
    Task<Result<string>> LogInAsync(LoginDto loginDto, CancellationToken cancellationToken = default);

    string GenerateAuthToken(User user, CancellationToken cancellationToken = default);

    UserClaimsDto? GetLoggedInUser();

    string HashPassword(string password);

    bool ValidatePassword(string password, string passwordHash);

    bool IsLoggedIn();
}