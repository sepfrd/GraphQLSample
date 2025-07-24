namespace Infrastructure.Services.AuthService.Dtos.SignupDto;

public record SignupDto(string Username, string Email, string Password, bool IsAdmin);