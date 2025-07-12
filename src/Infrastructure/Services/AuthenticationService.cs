using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Application.Abstractions;
using Application.Common.Constants;
using Application.EntityManagement.Roles.Queries;
using Application.EntityManagement.UserRoles.Queries;
using Application.EntityManagement.Users.Dtos;
using BCrypt.Net;
using Domain.Entities;
using Infrastructure.Common.Configurations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly ISender _sender;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JwtOptions _jwtOptions;
    private static SigningCredentials? _signingCredentials;

    public AuthenticationService(ISender sender, IHttpContextAccessor httpContextAccessor, IOptions<AppOptions> appOptions)
    {
        _sender = sender;
        _httpContextAccessor = httpContextAccessor;
        _jwtOptions = appOptions.Value.JwtOptions;

        if (_signingCredentials is not null)
        {
            return;
        }

        var rsa = RSA.Create();

        rsa.ImportFromPem(_jwtOptions.PrivateKey);

        var securityKey = new RsaSecurityKey(rsa);

        _signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha512Signature);
    }

    public async Task<string?> CreateJwtAsync(User user, CancellationToken cancellationToken = default)
    {
        var userRolesQuery = new GetAllUserRolesQuery(userRole => userRole.UserId == user.InternalId);

        var userRolesResult = await _sender.Send(userRolesQuery, cancellationToken);

        if (!userRolesResult.IsSuccessful ||
            userRolesResult.Data is null ||
            !userRolesResult.Data.Any())
        {
            return null;
        }

        var roleIds = userRolesResult.Data.Select(userRole => userRole.RoleId);

        var rolesQuery = new GetAllRolesQuery(role => roleIds.Contains(role.InternalId));

        var (roles, isSuccessful, _, _) = await _sender.Send(rolesQuery, cancellationToken);

        var rolesList = roles?.ToList();

        if (!isSuccessful ||
            rolesList is null ||
            rolesList.Count == 0)
        {
            return null;
        }

        var claims = new ClaimsIdentity(new[]
        {
            new Claim(JwtRegisteredClaimNames.Iss, _jwtOptions.Issuer),
            new Claim(JwtRegisteredClaimNames.Aud, _jwtOptions.Audience),
            new Claim(
                JwtRegisteredClaimNames.Iat,
                DateTime.Now.ToUniversalTime().ToString(CultureInfo.InvariantCulture)),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtClaimConstants.UsernameClaim, user.Username),
            new Claim(JwtClaimConstants.ExternalIdClaim, user.ExternalId.ToString(CultureInfo.InvariantCulture))
        });

        foreach (var role in rolesList)
        {
            var claim = new Claim(ClaimTypes.Role, role.Title);

            claims.AddClaim(claim);
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = DateTime.Now.AddMinutes(_jwtOptions.TokenExpirationDurationMinutes),
            SigningCredentials = _signingCredentials
        };

        var jwtHandler = new JwtSecurityTokenHandler();

        var token = jwtHandler.CreateToken(tokenDescriptor);

        var jwt = jwtHandler.WriteToken(token);

        return jwt;
    }

    public UserClaimsDto? GetLoggedInUser()
    {
        if (!IsLoggedIn())
        {
            return null;
        }

        var user = _httpContextAccessor.HttpContext!.User;

        var iss = user.FindFirstValue(JwtRegisteredClaimNames.Iss);
        var aud = user.FindFirstValue(JwtRegisteredClaimNames.Aud);
        var iat = user.FindFirstValue(JwtRegisteredClaimNames.Iat);
        var jti = user.FindFirstValue(JwtRegisteredClaimNames.Jti);
        var email = user.FindFirstValue(JwtRegisteredClaimNames.Email);
        var username = user.FindFirstValue(JwtClaimConstants.UsernameClaim);
        var externalIdString = user.FindFirstValue(JwtClaimConstants.ExternalIdClaim);
        var roles = user.FindFirstValue(ClaimTypes.Role)?.Split();

        int? externalId = externalIdString is null ? null : int.Parse(externalIdString, CultureInfo.InvariantCulture);

        var userClaimsDto = new UserClaimsDto(
            iss,
            aud,
            iat,
            jti,
            email,
            username,
            externalId,
            roles);

        return userClaimsDto;
    }

    public string HashPassword(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password, HashType.SHA512, 12);

    public bool ValidatePassword(string password, string passwordHash) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash, HashType.SHA512);

    public bool IsLoggedIn() =>
        _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
}