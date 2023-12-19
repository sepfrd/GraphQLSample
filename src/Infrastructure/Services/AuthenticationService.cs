using Application.Abstractions;
using Application.EntityManagement.Roles.Queries;
using Application.EntityManagement.UserRoles.Queries;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly ISender _sender;
    private readonly IConfiguration _configuration;

    public AuthenticationService(ISender sender, IConfiguration configuration)
    {
        _sender = sender;
        _configuration = configuration;
    }

    public async Task<string?> CreateJwtAsync(User user, CancellationToken cancellationToken = default)
    {
        var privateKey = _configuration.GetSection("JwtConfiguration").GetValue<string>("PrivateKey");

        var rsa = RSA.Create();

        rsa.ImportFromPem(privateKey);

        var securityKey = new RsaSecurityKey(rsa);

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha512Signature);

        var userRolesQuery = new GetAllUserRolesQuery(Pagination.MaxPagination,
            userRole => userRole.UserId == user.InternalId);

        var userRolesResult = await _sender.Send(userRolesQuery, cancellationToken);

        if (!userRolesResult.IsSuccessful ||
            userRolesResult.Data is null ||
            !userRolesResult.Data.Any())
        {
            return null;
        }

        var roleIds = userRolesResult.Data.Select(userRole => userRole.RoleId);

        var rolesQuery = new GetAllRolesQuery(Pagination.MaxPagination, role => roleIds.Contains(role.InternalId));

        var rolesResult = await _sender.Send(rolesQuery, cancellationToken);

        if (!rolesResult.IsSuccessful ||
            rolesResult.Data is null ||
            !rolesResult.Data.Any())
        {
            return null;
        }

        var roles = rolesResult.Data;

        var claims = new ClaimsIdentity(new[]
        {
            new Claim(JwtRegisteredClaimNames.Iss, DomainConstants.ApplicationUrl),
            new Claim(JwtRegisteredClaimNames.Aud, DomainConstants.ApplicationUrl),
            new Claim(JwtRegisteredClaimNames.Iat,
                DateTime.Now.ToUniversalTime()
                    .ToString(CultureInfo.InvariantCulture)),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("username", user.Username),
            new Claim("external_id", user.ExternalId.ToString())
        });

        foreach (var role in roles)
        {
            var claim = new Claim(ClaimTypes.Role, role.Title);

            claims.AddClaim(claim);
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = signingCredentials
        };

        var jwtHandler = new JwtSecurityTokenHandler();

        var token = jwtHandler.CreateToken(tokenDescriptor);

        var jwt = jwtHandler.WriteToken(token);

        return jwt;
    }
}