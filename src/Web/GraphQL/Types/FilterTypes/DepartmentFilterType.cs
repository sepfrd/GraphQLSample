using Application.Services.Departments.Dtos;
using HotChocolate.Data.Filters;

namespace Web.GraphQL.Types.FilterTypes;

public class DepartmentFilterType : FilterInputType<DepartmentDto>
{
    protected override void Configure(IFilterInputTypeDescriptor<DepartmentDto> descriptor)
    {
        descriptor
            .BindFieldsExplicitly()
            .AllowAnd(false)
            .AllowOr(false);

        descriptor
            .Field(departmentDto => departmentDto.Id)
            .Ignore();

        descriptor
            .Field(departmentDto => departmentDto.Employees)
            .Ignore();

        descriptor
            .Field(employee => employee.CreatedAt)
            .Type<DateTimeOperationFilterInputType>();

        descriptor
            .Field(employee => employee.UpdatedAt)
            .Type<DateTimeOperationFilterInputType>();

        descriptor
            .Field(department => department.Name)
            .Type<CustomStringOperationFilterType>();

        descriptor
            .Field(department => department.Description)
            .Type<CustomStringOperationFilterType>();
    }
}