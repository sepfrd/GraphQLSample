namespace Infrastructure.Services.AuthService.Dtos.UserDto;

public sealed record UserDto(
    Guid Uuid,
    string Username,
    string Email);