using Application.Common.Abstractions.Services;
using Application.Common.Dtos;

namespace Web.GraphQL;

public class Query
{
    public static async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(
        [Service] IServiceBase<EmployeeDto> employeeService,
        CancellationToken cancellationToken)
    {
        var result = await employeeService.GetAllAsync(cancellationToken);

        return result.Data!;
    }
}