using Domain.Abstractions;
using Infrastructure.Services.AuthService;
using Infrastructure.Services.AuthService.Dtos;
using Infrastructure.Services.AuthService.Dtos.LoginDto;
using Infrastructure.Services.AuthService.Dtos.SignupDto;
using Infrastructure.Services.AuthService.Dtos.UserDto;

namespace Infrastructure.Abstractions;

public interface IAuthService
{
    Task<DomainResult<string>> LogInAsync(LoginDto loginDto, CancellationToken cancellationToken = default);

    Task<DomainResult<UserDto>> SignUpAsync(SignupDto signupDto, CancellationToken cancellationToken = default);

    string GenerateAuthToken(User user, CancellationToken cancellationToken = default);

    UserClaimsDto? GetLoggedInUser();

    string HashPassword(string password);

    bool ValidatePassword(string password, string passwordHash);

    bool IsLoggedIn();
}