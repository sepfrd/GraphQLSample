using Application.Services.Departments;
using Application.Services.Departments.Dtos;
using Application.Services.Employees.Dtos;
using Domain.Entities;
using Humanizer;
using Web.GraphQL.Types.FilterTypes;

namespace Web.GraphQL.Types;

public class DepartmentType : ObjectType<DepartmentDto>
{
    protected override void Configure(IObjectTypeDescriptor<DepartmentDto> descriptor)
    {
        base.Configure(descriptor);

        descriptor
            .Name(nameof(Department).Pluralize())
            .Description("Represents a department within the organization, including its name and optional description.");

        descriptor
            .Field(departmentDto => departmentDto.Id)
            .Description("Unique identifier of the department.");

        descriptor
            .Field(departmentDto => departmentDto.CreatedAt)
            .Description("Timestamp indicating when the department was created.");

        descriptor
            .Field(departmentDto => departmentDto.UpdatedAt)
            .Description("Timestamp of the last update made to the department.");

        descriptor
            .Field(departmentDto => departmentDto.Name)
            .Description("The official name of the department.");

        descriptor
            .Field(departmentDto => departmentDto.Description)
            .Description("An optional textual description of the department's function or scope.");

        descriptor
            .Field(departmentDto => departmentDto.Employees)
            .Description("List of employees who are part of this department.")
            .ResolveWith<Resolvers>(_ =>
                Resolvers.GetEmployeesAsync(null!, null!, CancellationToken.None));
    }

    private sealed class Resolvers
    {
        public static async Task<IEnumerable<EmployeeDto>?> GetEmployeesAsync(
            [Parent] DepartmentDto departmentDto,
            [Service] IDepartmentService departmentService,
            CancellationToken cancellationToken = default)
        {
            var result = await departmentService.GetEmployeesByDepartmentIdAsync(departmentDto.Id, cancellationToken);

            return result.Data;
        }
    }
}