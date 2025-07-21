using Domain.Entities;
using HotChocolate.Data.Filters;

namespace Web.GraphQL.Types.FilterTypes;

public class EmployeeFilterType : FilterInputType<Employee>
{
    protected override void Configure(IFilterInputTypeDescriptor<Employee> descriptor)
    {
        descriptor
            .AllowAnd(false)
            .AllowOr(false);
    }
}