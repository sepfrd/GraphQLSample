using Application.EntityManagement.Users.Dtos;
using Domain.Entities;

namespace Application.Abstractions;

public interface IAuthenticationService
{
    Task<string?> CreateJwtAsync(User user, CancellationToken cancellationToken = default);

    UserClaimsDto? GetLoggedInUser();

    string HashPassword(string password);

    bool ValidatePassword(string password, string passwordHash);

    bool IsLoggedIn();
}