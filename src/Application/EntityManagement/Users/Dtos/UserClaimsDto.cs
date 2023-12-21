namespace Application.EntityManagement.Users.Dtos;

public record UserClaimsDto(
    string? Iss,
    string? Aud,
    string? Iat,
    string? Jti,
    string? Email,
    string? Username,
    int? ExternalId,
    string[]? Roles
);