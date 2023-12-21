using Application.EntityManagement.Users.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Abstractions;

public interface IAuthenticationService
{
    Task<string?> CreateJwtAsync(User user, CancellationToken cancellationToken = default);

    UserClaimsDto? GetLoggedInUser();

    bool IsLoggedIn();
}