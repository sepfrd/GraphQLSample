namespace Application.EntityManagement.Users.Dtos;

public record LoginDto(string UsernameOrEmail, string Password);