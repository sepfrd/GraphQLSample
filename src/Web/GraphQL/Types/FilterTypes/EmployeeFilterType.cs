using Application.Services.Employees.Dtos;
using Domain.Enums;
using HotChocolate.Data.Filters;

namespace Web.GraphQL.Types.FilterTypes;

public class EmployeeFilterType : FilterInputType<EmployeeDto>
{
    protected override void Configure(IFilterInputTypeDescriptor<EmployeeDto> descriptor)
    {
        descriptor
            .BindFieldsExplicitly()
            .AllowAnd(false)
            .AllowOr(false);

        descriptor
            .Field(employeeDto => employeeDto.Id)
            .Ignore();

        descriptor
            .Field(employeeDto => employeeDto.Projects)
            .Ignore();

        descriptor
            .Field(employeeDto => employeeDto.Department)
            .Ignore();

        descriptor
            .Field(employeeDto => employeeDto.CreatedAt)
            .Type<DateTimeOperationFilterInputType>();

        descriptor
            .Field(employeeDto => employeeDto.UpdatedAt)
            .Type<DateTimeOperationFilterInputType>();

        descriptor
            .Field(employeeDto => employeeDto.FirstName)
            .Type<CustomStringOperationFilterType>();

        descriptor
            .Field(employeeDto => employeeDto.LastName)
            .Type<CustomStringOperationFilterType>();

        descriptor
            .Field(employeeDto => employeeDto.Age)
            .Type<CustomIntOperationFilterType>();

        descriptor
            .Field(employeeDto => employeeDto.Status)
            .Type<EnumOperationFilterInputType<EmployeeStatus>>();

        descriptor
            .Field(employeeDto => employeeDto.Position)
            .Type<CustomStringOperationFilterType>();

        descriptor
            .Field(employeeDto => employeeDto.Skills)
            .Type<SkillFilterType>();
    }
}