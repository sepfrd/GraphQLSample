using Application.Common.Abstractions.Services;
using Application.Common.Dtos;
using Domain.Entities;

namespace Web.GraphQL;

public class Query
{
    public static async Task<IEnumerable<Employee>?> GetEmployeesAsync(
        [Service] IServiceBase<Employee, EmployeeDto> employeeService,
        CancellationToken cancellationToken)
    {
        var result = await employeeService.GetAllAsync(cancellationToken);
        return result;
    }
}