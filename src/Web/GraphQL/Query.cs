using Application.Services.Departments;
using Application.Services.Departments.Dtos;
using Application.Services.Employees;
using Application.Services.Employees.Dtos;
using Application.Services.Projects;
using Application.Services.Projects.Dtos;

namespace Web.GraphQL;

public class Query
{
    public static async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(
        [Service] IEmployeeService employeeService,
        CancellationToken cancellationToken)
    {
        var result = await employeeService.GetAllAsync(cancellationToken: cancellationToken);

        return result;
    }

    public static async Task<IEnumerable<ProjectDto>> GetProjectsAsync(
        [Service] IProjectService projectService,
        CancellationToken cancellationToken)
    {
        var result = await projectService.GetAllAsync(cancellationToken: cancellationToken);

        return result;
    }

    public static async Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync(
        [Service] IDepartmentService departmentService,
        CancellationToken cancellationToken)
    {
        var result = await departmentService.GetAllAsync(cancellationToken: cancellationToken);

        return result;
    }
}