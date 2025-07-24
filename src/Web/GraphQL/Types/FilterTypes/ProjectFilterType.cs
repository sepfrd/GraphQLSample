using Application.Services.Projects.Dtos;
using HotChocolate.Data.Filters;

namespace Web.GraphQL.Types.FilterTypes;

public class ProjectFilterType : FilterInputType<ProjectDto>
{
    protected override void Configure(IFilterInputTypeDescriptor<ProjectDto> descriptor)
    {
        descriptor
            .BindFieldsExplicitly()
            .AllowAnd(false)
            .AllowOr(false);

        descriptor
            .Field(projectDto => projectDto.Id)
            .Ignore();

        descriptor
            .Field(projectDto => projectDto.Employees)
            .Ignore();

        descriptor
            .Field(projectDto => projectDto.Manager)
            .Ignore();

        descriptor
            .Field(projectDto => projectDto.CreatedAt)
            .Type<DateTimeOperationFilterInputType>();

        descriptor
            .Field(projectDto => projectDto.UpdatedAt)
            .Type<DateTimeOperationFilterInputType>();

        descriptor
            .Field(projectDto => projectDto.Name)
            .Type<CustomStringOperationFilterType>();

        descriptor
            .Field(projectDto => projectDto.Description)
            .Type<CustomStringOperationFilterType>();
    }
}