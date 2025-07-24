using Domain.ValueObjects;
using HotChocolate.Data.Filters;

namespace Web.GraphQL.Types.FilterTypes;

public class SkillFilterType : FilterInputType<Skill>
{
    protected override void Configure(IFilterInputTypeDescriptor<Skill> descriptor)
    {
        base.Configure(descriptor);

        descriptor
            .Field(skill => skill.Name)
            .Type<CustomStringOperationFilterType>();
    }
}