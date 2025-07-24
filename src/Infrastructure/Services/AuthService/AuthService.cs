using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Common;
using BCrypt.Net;
using Domain.Abstractions;
using Infrastructure.Abstractions;
using Infrastructure.Common.Configurations;
using Infrastructure.Common.Constants;
using Infrastructure.Services.AuthService.Dtos;
using Infrastructure.Services.AuthService.Dtos.LoginDto;
using Infrastructure.Services.AuthService.Dtos.SignupDto;
using Infrastructure.Services.AuthService.Dtos.UserDto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JwtOptions _jwtOptions;
    private readonly IRepositoryBase<User> _userRepository;
    private readonly IMappingService _mappingService;

    private static SigningCredentials? _signingCredentials;

    public AuthService(
        IHttpContextAccessor httpContextAccessor,
        IOptions<AppOptions> appOptions,
        IRepositoryBase<User> userRepository,
        IMappingService mappingService)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
        _mappingService = mappingService;
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

    public async Task<DomainResult<string>> LogInAsync(LoginDto loginDto, CancellationToken cancellationToken = default)
    {
        if (IsLoggedIn())
        {
            return DomainResult<string>.Failure(new Error(MessageConstants.AlreadyLoggedIn), StatusCodes.Status400BadRequest);
        }

        var users = await _userRepository.GetAllAsync(
            user => user.Username == loginDto.UsernameOrEmail || user.Email == loginDto.UsernameOrEmail,
            cancellationToken);

        var user = users.FirstOrDefault();

        if (user is null)
        {
            return DomainResult<string>.Failure(new Error(MessageConstants.InvalidCredentials), StatusCodes.Status400BadRequest);
        }

        var isPasswordValid = ValidatePassword(loginDto.Password, user.PasswordHash);

        if (!isPasswordValid)
        {
            return DomainResult<string>.Failure(new Error(MessageConstants.InvalidCredentials), StatusCodes.Status400BadRequest);
        }

        var jwt = GenerateAuthToken(user, cancellationToken);

        return DomainResult<string>.Success(jwt);
    }

    public async Task<DomainResult<UserDto>> SignUpAsync(SignupDto signupDto, CancellationToken cancellationToken = default)
    {
        var isUsernameValid = !(await _userRepository.GetAllAsync(
                user => user.Username == signupDto.Username,
                cancellationToken))
            .Any();

        if (!isUsernameValid)
        {
            return DomainResult<UserDto>.Failure(
                AuthErrors.PropertyNotUnique(nameof(SignupDto.Username), signupDto.Username),
                StatusCodes.Status400BadRequest);
        }

        var isEmailValid = !(await _userRepository.GetAllAsync(
                user => user.Email == signupDto.Email,
                cancellationToken))
            .Any();

        if (!isEmailValid)
        {
            return DomainResult<UserDto>.Failure(
                AuthErrors.PropertyNotUnique(nameof(SignupDto.Email), signupDto.Email),
                StatusCodes.Status400BadRequest);
        }

        var user = new User
        {
            Username = signupDto.Username,
            PasswordHash = HashPassword(signupDto.Password),
            Email = signupDto.Email,
            Roles = signupDto.IsAdmin ? [RoleConstants.User, RoleConstants.Admin] : [RoleConstants.User]
        };

        var createdUser = await _userRepository.CreateAsync(user, cancellationToken);

        if (createdUser is null)
        {
            return DomainResult<UserDto>.Failure(Errors.InternalServerError, StatusCodes.Status500InternalServerError);
        }

        var userDto = _mappingService.Map<User, UserDto>(createdUser);

        return DomainResult<UserDto>.Success(userDto, StatusCodes.Status201Created);
    }

    public string GenerateAuthToken(User user, CancellationToken cancellationToken = default)
    {
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
            new Claim(JwtClaimConstants.ExternalIdClaim, user.Id.ToString())
        });

        claims.AddClaims(user.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

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

        var userClaimsDto = new UserClaimsDto(
            iss,
            aud,
            iat,
            jti,
            email,
            username);

        return userClaimsDto;
    }

    public string HashPassword(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password, HashType.SHA512, 12);

    public bool ValidatePassword(string password, string passwordHash) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash, HashType.SHA512);

    public bool IsLoggedIn() =>
        _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
}