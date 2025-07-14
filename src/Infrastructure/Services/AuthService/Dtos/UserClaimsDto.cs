namespace Infrastructure.Services.AuthService.Dtos;

public record UserClaimsDto(
    string? Iss,
    string? Aud,
    string? Iat,
    string? Jti,
    string? Email,
    string? Username);