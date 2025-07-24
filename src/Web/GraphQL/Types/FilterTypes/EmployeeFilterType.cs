using Application.Services.Employees.Dtos;
using HotChocolate.Data.Filters;

namespace Web.GraphQL.Types.FilterTypes;

public class EmployeeFilterType : FilterInputType<EmployeeDto>
{
    protected override void Configure(IFilterInputTypeDescriptor<EmployeeDto> descriptor)
    {
        base.Configure(descriptor);

        descriptor
            .BindFieldsExplicitly()
            .AllowAnd(false)
            .AllowOr(false);

        descriptor
            .Field(employee => employee.Projects)
            .Type<ProjectFilterType>();

        descriptor
            .Field(employee => employee.Position)
            .Type<CustomStringOperationFilterType>();

        descriptor
            .Field(employee => employee.FirstName)
            .Type<CustomStringOperationFilterType>();

        descriptor
            .Field(employee => employee.LastName)
            .Type<CustomStringOperationFilterType>();

        descriptor
            .Field(employee => employee.Department)
            .Type<DepartmentFilterType>();

        descriptor
            .Field(employee => employee.Skills)
            .Type<SkillFilterType>();

        descriptor
            .Field(employee => employee.Age)
            .Type<CustomIntOperationFilterType>();
    }
}