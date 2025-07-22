using Application.Common.Abstractions.Services;
using Application.Common.Dtos;
using Domain.Abstractions;
using Infrastructure.Abstraction;
using Infrastructure.Services.AuthService.Dtos.LoginDto;

namespace Web.GraphQL;

public class Mutation
{
    public static async Task<DomainResult<string>> LoginAsync(
        [Service] IAuthService authService,
        LoginDto loginDto,
        CancellationToken cancellationToken) =>
        await authService.LogInAsync(loginDto, cancellationToken);

    public static async Task<DomainResult<EmployeeDto>> CreateEmployeeAsync(
        [Service] IServiceBase<EmployeeDto> employeeService,
        EmployeeDto employeeDto,
        CancellationToken cancellationToken) =>
        await employeeService.CreateOneAsync(employeeDto, cancellationToken);
}