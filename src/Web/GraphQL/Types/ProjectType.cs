using Application.Services.Employees.Dtos;
using Application.Services.Projects;
using Application.Services.Projects.Dtos;
using Domain.Entities;
using Humanizer;
using Web.GraphQL.Types.FilterTypes;

namespace Web.GraphQL.Types;

public class ProjectType : ObjectType<ProjectDto>
{
    protected override void Configure(IObjectTypeDescriptor<ProjectDto> descriptor)
    {
        base.Configure(descriptor);

        descriptor
            .Name(nameof(Project).Pluralize())
            .Description("Represents a project with its metadata, team members, and manager information.");

        descriptor
            .Field(projectDto => projectDto.Id)
            .Description("Unique identifier of the project.");

        descriptor
            .Field(projectDto => projectDto.CreatedAt)
            .Description("Timestamp of when the project was created.");

        descriptor
            .Field(projectDto => projectDto.UpdatedAt)
            .Description("Timestamp of the last update to the project.");

        descriptor
            .Field(projectDto => projectDto.Name)
            .Description("The name of the project.");

        descriptor
            .Field(projectDto => projectDto.Description)
            .Description("A brief description of the project's goals and content.");

        descriptor
            .Field(projectDto => projectDto.Manager)
            .Description("The employee who manages this project.")
            .ResolveWith<Resolvers>(_ =>
                Resolvers.GetManagerAsync(null!, null!, CancellationToken.None));

        descriptor
            .Field(projectDto => projectDto.Employees)
            .Description("A list of employees assigned to this project.")
            .ResolveWith<Resolvers>(_ =>
                Resolvers.GetEmployeesAsync(null!, null!, CancellationToken.None));
    }

    private sealed class Resolvers
    {
        public static async Task<IEnumerable<EmployeeDto>?> GetEmployeesAsync(
            [Parent] ProjectDto projectDto,
            [Service] IProjectService projectService,
            CancellationToken cancellationToken = default)
        {
            var result = await projectService.GetEmployeesByProjectIdAsync(projectDto.Id, cancellationToken);

            return result.Data;
        }

        public static async Task<EmployeeDto?> GetManagerAsync(
            [Parent] ProjectDto projectDto,
            [Service] IProjectService projectService,
            CancellationToken cancellationToken = default)
        {
            var result = await projectService.GetManagerByProjectIdAsync(projectDto.Id, cancellationToken);

            return result.Data;
        }
    }
}