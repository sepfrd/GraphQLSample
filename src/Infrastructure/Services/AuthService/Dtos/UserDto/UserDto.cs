namespace Infrastructure.Services.AuthService.Dtos.UserDto;

public sealed record UserDto(
    Guid Id,
    string Username,
    string Email);