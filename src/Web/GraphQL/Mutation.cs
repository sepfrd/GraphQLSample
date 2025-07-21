using Infrastructure.Abstraction;
using Infrastructure.Services.AuthService.Dtos.LoginDto;

namespace Web.GraphQL;

public class Mutation
{
    public static async Task<Infrastructure.Common.Dtos.Result<string>> LoginAsync(
        [Service] IAuthService authService,
        LoginDto loginDto,
        CancellationToken cancellationToken) =>
        await authService.LogInAsync(loginDto, cancellationToken);
}