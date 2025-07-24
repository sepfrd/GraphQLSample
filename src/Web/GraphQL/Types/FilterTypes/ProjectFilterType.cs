using Application.Services.Projects.Dtos;
using HotChocolate.Data.Filters;

namespace Web.GraphQL.Types.FilterTypes;

public class ProjectFilterType : FilterInputType<ProjectDto>
{
    protected override void Configure(IFilterInputTypeDescriptor<ProjectDto> descriptor)
    {
        base.Configure(descriptor);

        descriptor
            .Field(project => project.Name)
            .Type<CustomStringOperationFilterType>();

        descriptor
            .Field(project => project.Description)
            .Type<CustomStringOperationFilterType>();

        descriptor
            .Field(project => project.Employees)
            .Type<EmployeeFilterType>();

        descriptor
            .Field(project => project.Manager)
            .Type<EmployeeFilterType>();
    }
}