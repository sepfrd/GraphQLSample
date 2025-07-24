using Application.Services.Departments.Dtos;
using Application.Services.Employees;
using Application.Services.Employees.Dtos;
using Application.Services.Projects.Dtos;
using Domain.Entities;
using Humanizer;

namespace Web.GraphQL.Types;

public class EmployeeType : ObjectType<EmployeeDto>
{
    protected override void Configure(IObjectTypeDescriptor<EmployeeDto> descriptor)
    {
        descriptor
            .Name(nameof(Employee).Pluralize())
            .Description("Represents a simplified view of an employee with key personal and organizational information.");

        descriptor
            .Field(employee => employee.Id)
            .Description("The unique identifier (ID) of the employee.");

        descriptor
            .Field(employee => employee.CreatedAt)
            .Description("The timestamp when the employee record was created.");

        descriptor
            .Field(employee => employee.UpdatedAt)
            .Description("The timestamp of the last update to the employee record.");

        descriptor
            .Field(employee => employee.FirstName)
            .Description("The first name of the employee.");

        descriptor
            .Field(employee => employee.LastName)
            .Description("The last name (surname) of the employee.");

        descriptor
            .Field(employee => employee.Age)
            .Description("The age of the employee.");

        descriptor
            .Field(employee => employee.Status)
            .Description("The current employment status, such as Active or Inactive.");

        descriptor
            .Field(employee => employee.Position)
            .Description("The position or job title of the employee.");

        descriptor
            .Field(employee => employee.Skills)
            .Description("The list of skills associated with the employee.");

        descriptor
            .Field(employee => employee.Department)
            .ResolveWith<Resolvers>(_ =>
                Resolvers.GetDepartmentAsync(null!, null!, CancellationToken.None))
            .Description("The department this employee belongs to.");

        descriptor
            .Field(employee => employee.Projects)
            .Description("The list of projects associated with the employee.")
            .ResolveWith<Resolvers>(_ =>
                Resolvers.GetProjectsAsync(null!, null!, CancellationToken.None))
            .UsePaging();
    }

    private sealed class Resolvers
    {
        public static async Task<IEnumerable<ProjectDto>?> GetProjectsAsync(
            [Parent] EmployeeDto employee,
            [Service] IEmployeeService employeeService,
            CancellationToken cancellationToken = default)
        {
            var result = await employeeService.GetProjectsByEmployeeIdAsync(employee.Id, cancellationToken);

            return result.Data;
        }

        public static async Task<DepartmentDto?> GetDepartmentAsync(
            [Parent] EmployeeDto employee,
            [Service] IEmployeeService employeeService,
            CancellationToken cancellationToken = default)
        {
            var result = await employeeService.GetDepartmentByEmployeeIdAsync(employee.Id, cancellationToken);

            return result.Data;
        }
    }
}