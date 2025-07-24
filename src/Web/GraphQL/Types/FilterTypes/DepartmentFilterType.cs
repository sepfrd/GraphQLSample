using Application.Services.Departments.Dtos;
using HotChocolate.Data.Filters;

namespace Web.GraphQL.Types.FilterTypes;

public class DepartmentFilterType : FilterInputType<DepartmentDto>
{
    protected override void Configure(IFilterInputTypeDescriptor<DepartmentDto> descriptor)
    {
        base.Configure(descriptor);

        descriptor
            .Field(department => department.Name)
            .Type<CustomStringOperationFilterType>();

        descriptor
            .Field(department => department.Description)
            .Type<CustomStringOperationFilterType>();
    }
}